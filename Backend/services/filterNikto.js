// services/filterNikto.js

function filterNikto(niktoData) {

  const filtered =
    niktoData.findings
      .slice(0,20);

  return {
    vulnerabilities: filtered
  };
}

module.exports = filterNikto;