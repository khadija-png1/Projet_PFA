// services/parseNikto.js

const fs = require("fs");

function parseNikto(filePath) {

  try {

    const content = fs.readFileSync(filePath, "utf8");

    const findings = [];

    const lines = content.split("\n");

    for (const line of lines) {

      if (
        line.includes("OSVDB") ||
        line.includes("+ ")
      ) {

        findings.push(
          line.trim()
        );
      }
    }

    return {
      findings
    };

  } catch {

    return {
      findings:[]
    };
  }
}

module.exports = parseNikto;