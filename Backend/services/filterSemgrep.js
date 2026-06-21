function filterSemgrep(data) {

  const severityMap = {
    ERROR: 3,
    WARNING: 2,
    INFO: 1
  };

  const findings =
    (data.findings || [])
      .map(f => ({
        ...f,
        score:
          severityMap[
            (f.severity || "").toUpperCase()
          ] || 0
      }))
      .filter(f => f.score >= 2);

  return {
    total: findings.length,
    findings
  };
}

module.exports = filterSemgrep;