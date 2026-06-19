const fs = require("fs");

function parseZapHtml(filePath) {
  try {
    const html = fs.readFileSync(filePath, "utf8");

    const alerts = [];

    // On cible uniquement la table "Alertes"
    const alertSection = html.split("Alertes")[1];
    if (!alertSection) return { alerts: [] };

    const rowRegex = /<tr[^>]*>\s*<td[^>]*>(.*?)<\/td>\s*<td[^>]*>(.*?)<\/td>\s*<td[^>]*>(.*?)<\/td>/g;

    let match;

    while ((match = rowRegex.exec(alertSection)) !== null) {
      const name = match[1]?.replace(/<[^>]+>/g, "").trim();
      const risk = match[2]?.replace(/<[^>]+>/g, "").trim();
      const instances = match[3]?.replace(/<[^>]+>/g, "").trim();

      // skip header junk
      if (!name || name.toLowerCase().includes("nom")) continue;

      alerts.push({
        name,
        risk,
        instances: Number(instances) || 1
      });
    }

    return {
      alerts: alerts.slice(0, 50)
    };

  } catch (e) {
    console.error("parseZapHtml error:", e.message);
    return { alerts: [] };
  }
}

module.exports = parseZapHtml;