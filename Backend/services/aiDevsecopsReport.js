const Groq = require("groq-sdk");

const groq = new Groq({
  apiKey: process.env.GROQ_API_KEY_DEV
});

async function analyzeDevsecops(data) {

  const prompt = `

You are a senior DevSecOps security auditor.

You MUST NOT conclude "0 risk" or "100/100 secure" unless:
- code is trivial AND
- no suspicious patterns exist AND
- no insecure dependencies AND
- no secrets AND
- no risky code patterns

INPUT:
{devsecopsData}

TASK:
1. Analyze Trivy + Semgrep results
2. Even if findings = 0, infer possible risks from patterns
3. Map to OWASP Top 10
4. Assign realistic security score (0-100)

SCORING RULES:
- 90-100 = very mature enterprise secure system
- 70-89 = good but minor risks likely
- 40-69 = medium risk system
- 0-39 = highly vulnerable system

IMPORTANT:
If no findings exist, DO NOT automatically give 100/100.
You must reason about possible blind spots.

OUTPUT FORMAT:
- Executive Summary
- Risk Breakdown
- OWASP Mapping
- CWE Mapping
- Technical Recommendations
- CI/CD Security Recommendations
- Final Score (realistic)
${JSON.stringify(data, null, 2)}

`;
  const result = await groq.chat.completions.create({
    model: "llama-3.3-70b-versatile",
    messages: [{ role: "user", content: prompt }],
    temperature: 0.2
  });
  return result.choices[0].message.content;
}

module.exports = analyzeDevsecops;