// services/buildPromptData.js

function buildPromptData(
  nmap,
  nikto,
  zap
) {

  return {

    openPorts:
      nmap.openPorts || [],

    niktoFindings:
      nikto.vulnerabilities || [],

    zapFindings:
      zap.vulnerabilities || []
  };
}

module.exports = buildPromptData;