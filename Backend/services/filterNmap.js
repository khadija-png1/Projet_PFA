// services/filterNmap.js

function filterNmap(nmapData) {

  const openPorts = (nmapData.ports || [])
    .filter(p => p.state === "open");

  return {
    openPortsCount: openPorts.length,
    openPorts
  };
}

module.exports = filterNmap;