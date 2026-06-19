const PDFDocument = require("pdfkit");
const fs = require("fs");
const path = require("path");

async function generatePDF(text, outputPath) {

  const fullPath = path.isAbsolute(outputPath)
    ? outputPath
    : path.join(__dirname, "..", outputPath);

  fs.mkdirSync(path.dirname(fullPath), {
    recursive: true
  });

  const doc = new PDFDocument({
    margin: 40,
    size: "A4"
  });

  const stream = fs.createWriteStream(fullPath);

  doc.pipe(stream);

  // Header
  doc
    .fontSize(22)
    .text("PENTEST SECURITY REPORT", {
      align: "center"
    });

  doc.moveDown();

  doc
    .fontSize(10)
    .text(`Generated: ${new Date().toLocaleString()}`);

  doc.moveDown(2);

  const lines = text.split("\n");

  lines.forEach(line => {

    const clean = line.trim();

    if (
      clean.includes("Executive Summary") ||
      clean.includes("Vulnérabilités") ||
      clean.includes("Analyse des risques") ||
      clean.includes("Recommandations") ||
      clean.includes("Conclusion")
    ) {

      doc.moveDown();

      doc
        .fontSize(16)
        .text(clean);

      doc.moveDown(0.5);

    } else {

      doc
        .fontSize(11)
        .text(clean, {
          lineGap: 4
        });

    }

  });

  doc.end();

  return new Promise((resolve, reject) => {

    stream.on("finish", () => {
      resolve(fullPath);
    });

    stream.on("error", reject);

  });

}

module.exports = generatePDF;