const fs = require("fs");

function parseTrivy(file) {
  try {

    const json =
      JSON.parse(fs.readFileSync(file, "utf8"));

    const vulns = [];

    for (const result of json.Results || []) {

      for (const vuln of result.Vulnerabilities || []) {

        vulns.push({
          id: vuln.VulnerabilityID,
          package: vuln.PkgName,
          severity: vuln.Severity,
          title: vuln.Title,
          fixedVersion: vuln.FixedVersion || "N/A"
        });

      }
    }

    return {
      vulnerabilities: vulns
    };

  } catch (e) {

    return {
      vulnerabilities: []
    };

  }
}

module.exports = parseTrivy;