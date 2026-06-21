const fs = require("fs");

function parseSemgrep(file) {

  try {

    const json =
      JSON.parse(fs.readFileSync(file, "utf8"));

    const findings = [];

    for (const r of json.results || []) {

      findings.push({
        rule: r.check_id,
        severity: r.extra.severity,
        message: r.extra.message,
        file: r.path,
        line: r.start.line
      });

    }

    return {
      findings
    };

  } catch (e) {

    return {
      findings: []
    };

  }
}

module.exports = parseSemgrep;