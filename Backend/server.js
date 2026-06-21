require("dotenv").config();

const { supabase } = require("./config/supabaseClient");

const express = require("express");
const cors = require("cors");
const { exec } = require("child_process");
const fs = require("fs");
const path = require("path");
const multer = require("multer");
const util = require("util");

const execPromise = util.promisify(exec);

/* ================= SERVICES ================= */
const analyzePentest = require("./services/aiReport");
const generatePDF = require("./services/generatePDF");

const parseNmap = require("./services/nmapParser");
const parseZapHtml = require("./services/parseZapHtml");
const parseNikto = require("./services/parseNikto");

const filterNmap = require("./services/filterNmap");
const filterZap = require("./services/filterZap");
const filterNikto = require("./services/filterNikto");

const parseTrivy = require("./services/parseTrivy");
const parseSemgrep = require("./services/parseSemgrep");

const filterTrivy = require("./services/filterTrivy");
const filterSemgrep = require("./services/filterSemgrep");

const analyzeDevsecops = require("./services/aiDevsecopsReport");

const app = express();

/* ================= MIDDLEWARE ================= */
app.use(cors());
app.use(express.json());

app.use("/reports", express.static(path.join(__dirname, "reports")));

const upload = multer({ dest: "uploads/" });

/* ==================================================
   GET LAST FILE
================================================== */
function getLatestFile(dir, prefix, ext) {
  if (!fs.existsSync(dir)) {
    throw new Error(`Directory not found: ${dir}`);
  }

  const files = fs.readdirSync(dir).filter((f) =>
    f.startsWith(prefix) && f.endsWith(ext)
  );

  if (!files.length) {
    throw new Error(`No file found: ${prefix}`);
  }

  files.sort(
    (a, b) =>
      fs.statSync(path.join(dir, b)).mtimeMs -
      fs.statSync(path.join(dir, a)).mtimeMs
  );

  return path.join(dir, files[0]);
}

/* ==================================================
   PENTEST ROUTE
================================================== */
app.post("/pentest/scan", async (req, res) => {
  const target = req.body.target?.trim();

  if (!target) {
    return res.status(400).json({ error: "target missing" });
  }

  const scriptPath =
    "D:/Projet_PFA/Scripts/Pentesting/pentest.sh";

  const cmd =
    `"C:/Program Files/Git/bin/bash.exe" "${scriptPath}" "${target}"`;

  exec(cmd, async (err) => {
    if (err) return res.status(500).json({ error: err.message });

    try {
      const resultDir =
        "D:/Projet_PFA/Scripts/Pentesting/results_pentest";

      const nmapFile = getLatestFile(resultDir, "nmap-", ".xml");
      const zapFile = getLatestFile(resultDir, "zap-", ".html");

      let niktoFile = null;
      try {
        niktoFile = getLatestFile(resultDir, "nikto-", ".txt");
      } catch { }

      const nmapRaw = parseNmap(nmapFile);
      const zapRaw = parseZapHtml(zapFile);
      const niktoRaw = niktoFile
        ? parseNikto(niktoFile)
        : { findings: [] };

      const pentestData = {
        nmap: filterNmap(nmapRaw),
        zap: filterZap(zapRaw),
        nikto: niktoFile ? filterNikto(niktoRaw) : [],
      };

      const report = await analyzePentest(pentestData);

      const pdfPath = path.join(
        __dirname,
        "reports",
        `pentest-${Date.now()}.pdf`
      );

      fs.mkdirSync(path.dirname(pdfPath), { recursive: true });
      await generatePDF(report, pdfPath);

      /* ================= SUPABASE INSERT ================= */

      const { data: project, error: projectErr } =
        await supabase
          .from("project")
          .insert([
            {
              title: "Pentest Scan",
              description: "Automated pentest scan",
              scan_type: "PENTEST",
              target: target,
              vulnerabilities_count:
                report.total_vulnerabilities || 0,
              severity_score: report.score || 50,
            },
          ])
          .select()
          .single();

      if (projectErr) throw projectErr;

      const { data: scan, error: scanErr } =
        await supabase
          .from("scan")
          .insert([
            {
              type_scan: "PENTEST",
              target_type: "URL",
              status: "success",
              started_at: new Date(),
              finished_at: new Date(),
              id_project: project.id,
            },
          ])
          .select()
          .single();

      if (scanErr) throw scanErr;

      const { error: reportErr } = await supabase
        .from("report")
        .insert([
          {
            report_name: `pentest-${Date.now()}`,
            report_type: "PENTEST",
            report_path: pdfPath,
            total_vulnerabilities:
              report.total_vulnerabilities || 0,
            tool: "AI",
            summary: report.summary,
            id_scan: scan.id,
          },
        ]);

      if (reportErr) throw reportErr;

      return res.json({
        success: true,
        report,
        pdf: `http://localhost:4000/reports/${path.basename(
          pdfPath
        )}`,
      });
    } catch (error) {
      return res.status(500).json({ error: error.message });
    }
  });
});

/* ==================================================
   SAST ROUTE
================================================== */
app.post("/scan", upload.single("file"), (req, res) => {
  const zipPath = req.file.path;
  const extractPath = path.join(__dirname, "project");

  fs.rmSync(extractPath, { recursive: true, force: true });
  fs.mkdirSync(extractPath, { recursive: true });

  fs.createReadStream(zipPath)
    .pipe(
      require("unzipper").Extract({ path: extractPath })
    )
    .on("close", async () => {
      try {
        const trivyScript =
          "D:/Projet_PFA/Scripts/DevSecOps/SAST/trivy.sh";

        const semgrepScript =
          "D:/Projet_PFA/Scripts/DevSecOps/SAST/semgrep.sh";

        const bash =
          `"C:/Program Files/Git/bin/bash.exe"`;

        await execPromise(
          `${bash} "${trivyScript}" "${extractPath}"`
        );

        await execPromise(
          `${bash} "${semgrepScript}" "${extractPath}"`
        );

        const resultDir =
          "D:/Projet_PFA/Scripts/DevSecOps/results";

        const trivyFile = path.join(
          resultDir,
          "trivy.json"
        );

        const semgrepFile = path.join(
          resultDir,
          "semgrep.json"
        );

        const devsecopsData = {
          trivy: filterTrivy(parseTrivy(trivyFile)),
          semgrep: filterSemgrep(parseSemgrep(semgrepFile)),
        };

        const report =
          await analyzeDevsecops(devsecopsData);

        const pdfPath = path.join(
          __dirname,
          "reports",
          `devsecops-${Date.now()}.pdf`
        );

        fs.mkdirSync(path.dirname(pdfPath), {
          recursive: true,
        });

        await generatePDF(report, pdfPath);

        /* ================= SUPABASE ================= */

        const { data: project, error: projectErr } =
          await supabase
            .from("project")
            .insert([
              {
                title: "SAST Scan",
                description: "ZIP analysis",
                scan_type: "SAST",
                target: extractPath,
                vulnerabilities_count:
                  report.total_vulnerabilities || 0,
                severity_score: report.score || 70,
              },
            ])
            .select()
            .single();

        if (projectErr) throw projectErr;

        const { data: scan, error: scanErr } =
          await supabase
            .from("scan")
            .insert([
              {
                type_scan: "SAST",
                target_type: "ZIP",
                status: "success",
                started_at: new Date(),
                finished_at: new Date(),
                id_project: project.id,
              },
            ])
            .select()
            .single();

        if (scanErr) throw scanErr;

        const { error: reportErr } = await supabase
          .from("report")
          .insert([
            {
              report_name: `devsecops-${Date.now()}`,
              report_type: "SAST",
              report_path: pdfPath,
              total_vulnerabilities:
                report.total_vulnerabilities || 0,
              tool: "AI",
              summary: report.summary,
              id_scan: scan.id,
            },
          ]);

        if (reportErr) throw reportErr;

        return res.json({
          success: true,
          report,
          pdf: `http://localhost:4000/reports/${path.basename(
            pdfPath
          )}`,
        });
      } catch (e) {
        return res.status(500).json({ error: e.message });
      }
    });
});

/* ==================================================
   FILE VIEWER
================================================== */
app.get("/file", (req, res) => {
  const filePath = req.query.path;

  if (!filePath)
    return res.status(400).send("Missing file path");

  res.sendFile(path.resolve(filePath));
});

/* ==================================================
   START SERVER
================================================== */
app.listen(4000, () => {
  console.log("Server running on http://localhost:4000");
});