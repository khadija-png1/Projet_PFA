import "../style/table.css";
import "../style/scan.css";

import Form from "react-bootstrap/Form";
import React, { useState } from "react";

function DevSecOps() {
  const [file, setFile] = useState(null);
  const [result, setResult] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleFileChange = (e) => {
    console.log("FILE SELECTED:", e.target.files[0]);
    setFile(e.target.files[0]);
  };

  const handleScan = async () => {
    if (!file) {
      alert("Select ZIP first");
      return;
    }

    const formData = new FormData();
    formData.append("file", file);

    setLoading(true);

    try {
      const res = await fetch("http://localhost:4000/scan", {
        method: "POST",
        body: formData,
      });

      const data = await res.json();

      console.log("SCAN RESULT:", data);
      setResult(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container-fluid py-5">
      <div className="upload-card">

        <h2>DevSecOps Scan</h2>
        <p>Importer un fichier ZIP</p>

        <Form.Group className="mb-4">
          <Form.Label>Fichier ZIP</Form.Label>

          <Form.Control
            type="file"
            onChange={handleFileChange}
          />
        </Form.Group>

        <button
          className="btn btn-success"
          onClick={handleScan}
          disabled={loading}
        >
          {loading ? "Scan en cours..." : "Lancer scan DevSecOps"}
        </button>

      </div>

      {result && (
        <div className="mt-4">
          <h4>Scan Result</h4>

          <pre style={{ background: "#111", color: "#0f0", padding: "10px" }}>
            {JSON.stringify(result, null, 2)}
          </pre>
        </div>
      )}
    </div>
  );
}

export default DevSecOps;