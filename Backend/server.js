require("dotenv").config();

const express = require("express");
const cors = require("cors");
const { exec } = require("child_process");
const fs = require("fs");
const path = require("path");
const multer = require("multer");

const analyzePentest = require("./services/aiReport");
const generatePDF = require("./services/generatePDF");

const parseNmap = require("./services/nmapParser");
const parseZapHtml = require("./services/parseZapHtml");
const parseNikto = require("./services/parseNikto");

const filterNmap = require("./services/filterNmap");
const filterZap = require("./services/filterZap");
const filterNikto = require("./services/filterNikto");

const app = express();

app.use(cors());
app.use(express.json());

app.use(
  "/reports",
  express.static(path.join(__dirname, "reports"))
);

const upload = multer({ dest: "uploads/" });

/* ==================================================
   GET LAST FILE HELPER
================================================== */
function getLatestFile(dir, prefix, ext) {
  if (!fs.existsSync(dir)) {
    throw new Error(`Directory not found: ${dir}`);
  }

  const files = fs.readdirSync(dir)
    .filter(f =>
      f.startsWith(prefix) &&
      (f.endsWith(ext) || f.endsWith(ext + "*"))
    );

  if (files.length === 0) {
    throw new Error(`No file found: ${prefix}*${ext}`);
  }

  files.sort((a, b) =>
    fs.statSync(path.join(dir, b)).mtimeMs -
    fs.statSync(path.join(dir, a)).mtimeMs
  );

  return path.join(dir, files[0].replace("*", ""));
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

  exec(cmd, async (err, stdout, stderr) => {

    console.log(stdout);
    console.log(stderr);

    if (err) {
      return res.status(500).json({ error: err.message });
    }

    try {

      const resultDir =
        "D:/Projet_PFA/Scripts/Pentesting/results_pentest";

      /* =========================
         GET LAST GENERATED FILES
      ========================= */
      const nmapFile = getLatestFile(resultDir, "nmap-", ".xml");
      const zapFile = getLatestFile(resultDir, "zap-", ".html");

      let niktoFile = null;
      try {
        niktoFile = getLatestFile(resultDir, "nikto-", ".txt");
      } catch (e) {
        console.log("[WARN] Nikto not found, skipping...");
      }

      console.log("NMAP:", nmapFile);
      console.log("ZAP:", zapFile);
      if (niktoFile) console.log("NIKTO:", niktoFile);

      /* =========================
         PARSING
      ========================= */
      const nmapRaw = parseNmap(nmapFile);
      const zapRaw = parseZapHtml(zapFile);
      const niktoRaw = niktoFile ? parseNikto(niktoFile) : { findings: [] };

      /* =========================
         FILTERING (IA READY)
      ========================= */
      const pentestData = {
        nmap: filterNmap(nmapRaw),
        zap: filterZap(zapRaw),
        nikto: niktoFile ? filterNikto(niktoRaw) : []
      };

      console.log("CLEAN DATA:");
      console.log(JSON.stringify(pentestData, null, 2));

      /* =========================
         AI REPORT
      ========================= */
      const report = await analyzePentest(pentestData);

      /* =========================
         PDF GENERATION
      ========================= */
      const reportsDir =
        path.join(__dirname, "reports");

      fs.mkdirSync(reportsDir, { recursive: true });

      const pdfPath =
        path.join(reportsDir, `report-${Date.now()}.pdf`);

      await generatePDF(report, pdfPath);

      return res.json({
        success: true,
        report,
        pdf: `http://localhost:4000/reports/${path.basename(pdfPath)}`
      });

    } catch (error) {
      console.log("SERVER ERROR:", error);

      return res.status(500).json({
        error: error.message
      });
    }
  });
});

/* ==================================================
   SAST ROUTE (UNCHANGED)
================================================== */
app.post("/scan", upload.single("file"), (req, res) => {

  const zipPath = req.file.path;
  const extractPath = path.join(__dirname, "project");

  fs.rmSync(extractPath, { recursive: true, force: true });
  fs.mkdirSync(extractPath, { recursive: true });

  fs.createReadStream(zipPath)
    .pipe(require("unzipper").Extract({ path: extractPath }))
    .on("close", () => {

      const findings = [];

      const command =
        process.platform === "win32"
          ? `findstr /S /I "eval exec password secret api_key token" ${extractPath}\\*.*`
          : `grep -r "eval\\|exec\\|password\\|secret\\|api_key\\|token" ${extractPath}`;

      exec(command, (err, stdout) => {

        if (stdout) {
          stdout.split("\n")
            .filter(Boolean)
            .forEach(line => {
              findings.push({
                severity: "medium",
                type: "pattern",
                details: line
              });
            });
        }

        return res.json({
          success: true,
          findings
        });
      });
    });
});

/* ==================================================
   FILE VIEWER
================================================== */
app.get("/file", (req, res) => {

  const filePath = req.query.path;

  if (!filePath) {
    return res.status(400).send("Missing file path");
  }

  res.sendFile(path.resolve(filePath));
});

/* ==================================================
   START SERVER
================================================== */
app.listen(4000, () => {
  console.log("Server running on http://localhost:4000");
});