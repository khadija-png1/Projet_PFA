function filterZap(zapData) {
  const severityMap = {
    haut: 3,
    high: 3,

    moyen: 2,
    medium: 2,

    faible: 1,
    low: 1,

    "pour information": 0,
    info: 0
  };

  const filtered = (zapData.alerts || [])
    .map(v => {
      const risk = (v.risk || "").toLowerCase().trim();

      return {
        name: v.name,
        risk,
        instances: v.instances || 1,
        score: severityMap[risk] ?? 0
      };
    })
    .filter(v => v.score >= 2) // garde Moyen + Haut uniquement
    .sort((a, b) => b.score - a.score);

  return {
    total: filtered.length,
    vulnerabilities: filtered
  };
}

module.exports = filterZap;