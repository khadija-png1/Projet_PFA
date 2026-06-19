const fs = require("fs");
const { XMLParser } = require("fast-xml-parser");

function parseNmap(xmlPath) {
  const xml = fs.readFileSync(xmlPath, "utf8");

  const parser = new XMLParser({
    ignoreAttributes: false,
    attributeNamePrefix: "@_"
  });

  const data = parser.parse(xml);

  const host = data?.nmaprun?.host;

  const ports = [];

  if (!host) {
    return { ports: [] };
  }

  const rawPorts = host?.ports?.port;

  //  NORMALISATION (IMPORTANT FIX)
  const portArray =
    Array.isArray(rawPorts)
      ? rawPorts
      : rawPorts
      ? [rawPorts]
      : [];

  for (const p of portArray) {
    ports.push({
      port: p?.["@_portid"] || "unknown",
      service: p?.service?.["@_name"] || "unknown",
      state: p?.state?.["@_state"] || "unknown"
    });
  }

  return { ports };
}

module.exports = parseNmap;