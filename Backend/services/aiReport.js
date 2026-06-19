const Groq = require("groq-sdk");

const groq = new Groq({
  apiKey: process.env.GROQ_API_KEY,
});

async function analyzePentest(pentestData) {

  const prompt = `
Tu es un expert en cybersécurité (niveau SOC / Red Team).

Analyse les résultats de pentest ci-dessous et génère un RAPPORT PROFESSIONNEL TRÈS DÉTAILLÉ.

=====================
📌 DONNÉES BRUTES
=====================

NMAP:
${JSON.stringify(pentestData.nmap, null, 2)}

ZAP:
${JSON.stringify(pentestData.zap, null, 2)}

NIKTO:
${JSON.stringify(pentestData.nikto, null, 2)}

=====================
📌 INSTRUCTIONS
=====================

Tu dois :

1. Identifier TOUTES les vulnérabilités (même faibles)
2. Ne jamais dire "aucune vulnérabilité" si des alertes existent
3. Classer les risques :
   - Critical
   - High
   - Medium
   - Low
   - Info

4. Ajouter :
   - Impact technique
   - Impact business
   - Exploit scenario (court)
   - Fix recommandé clair

=====================
📌 FORMAT OBLIGATOIRE
=====================

# 1. Executive Summary
# 2. Attack Surface (ports/services)
# 3. Vulnerabilities Breakdown
# 4. Risk Analysis (table)
# 5. Remediation Plan (step by step)
# 6. Final Security Score (0-100)

Rédige comme un rapport entreprise (très professionnel).
`;

  const response = await groq.chat.completions.create({
    model: "llama-3.3-70b-versatile",
    messages: [{ role: "user", content: prompt }],
    temperature: 0.2
  });

  return response.choices[0].message.content;
}

module.exports = analyzePentest;