const express = require("express");
const multer = require("multer");
const cors = require("cors");
const unzipper = require("unzipper");
const path = require("path");
const fs = require("fs");
const { exec } = require("child_process");

const app = express();
app.use(cors());

const upload = multer({ dest: "uploads/" });

/* =========================
   SAST SCAN ONLY
========================= */
app.post("/scan", upload.single("file"), async (req, res) => {
  try {
    const zipPath = req.file.path;

    const extractPath = path.join(__dirname, "project");
    const resultDir = path.join(__dirname, "results");

    // clean folders
    fs.rmSync(extractPath, { recursive: true, force: true });
    fs.mkdirSync(extractPath, { recursive: true });
    fs.mkdirSync(resultDir, { recursive: true });

    console.log("ZIP received:", zipPath);

    // unzip project
    fs.createReadStream(zipPath)
      .pipe(unzipper.Extract({ path: extractPath }))
      .on("close", () => {

        console.log("Project extracted");

        const outputFile = path.join(
          resultDir,
          `scan-${Date.now()}.json`
        );

        const scanData = {
          type: "sast",
          project: req.file.originalname,
          time: new Date().toISOString(),
          findings: []
        };

        // =========================
        // SAST COMMAND (cross platform)
        // =========================
        const command = process.platform === "win32"
          ? `findstr /S /I "eval exec password secret api_key token" ${extractPath}\\*.*`
          : `grep -r "eval\\|exec\\|password\\|secret\\|api_key\\|token" ${extractPath}`;

        exec(command, (err, stdout, stderr) => {

          const findings = [];

          // =========================
          // parse stdout
          // =========================
          if (stdout && stdout.length > 0) {
            stdout
              .split("\n")
              .filter(Boolean)
              .forEach(line => {
                findings.push({
                  type: "pattern-detection",
                  severity: "medium",
                  details: line
                });
              });
          }

          // =========================
          // handle errors
          // =========================
          if (err && !stdout) {
            findings.push({
              type: "scan-error",
              severity: "low",
              details: stderr || err.message
            });
          }

          // update final result
          scanData.findings = findings;

          // save JSON file
          fs.writeFileSync(outputFile, JSON.stringify(scanData, null, 2));

          console.log("Scan saved:", outputFile);

          // return to frontend
          return res.json({
            success: true,
            type: "SAST",
            file: outputFile,
            data: scanData
          });
        });

      })
      .on("error", (err) => {
        return res.status(500).json({
          success: false,
          error: "ZIP extraction failed",
          details: err.message
        });
      });

  } catch (err) {
    return res.status(500).json({
      success: false,
      error: err.message
    });
  }
});

/* =========================
   TEST ROUTE
========================= */
app.get("/", (req, res) => {
  res.send("SAST backend running 🚀");
});

/* =========================
   START SERVER
========================= */
app.listen(4000, () => {
  console.log("SAST running on http://localhost:4000");
});