function filterTrivy(data) {

  const severityMap = {
    CRITICAL: 4,
    HIGH: 3,
    MEDIUM: 2,
    LOW: 1
  };

  const vulns = (data.vulnerabilities || [])
    .map(v => ({
      ...v,
      score: severityMap[v.severity] || 0
    }))
    .filter(v => v.score >= 2)
    .sort((a,b) => b.score - a.score);

  return {
    total: vulns.length,
    vulnerabilities: vulns
  };
}

module.exports = filterTrivy;