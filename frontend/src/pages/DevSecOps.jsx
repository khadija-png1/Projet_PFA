import "../style/table.css";
import "../style/scan.css";

import Form from 'react-bootstrap/Form';
import React from "react";

import ButtonScan from "../components/ButtonScan";

function DevSecOps() {

  return (
    <div className="container-fluid py-5">

      <div className="upload-card">

        <div className="upload-header">
          <div className="upload-icon-container upload-icon">

            <i className="fas fa-code " />
          </div>
          <div>
            <h2 className="upload-title">
              DevSecOps Scan
            </h2>

            <p className="upload-subtitle">
              Importez un dossier zip ou un fichier
            </p>
          </div>
        </div>

  <Form.Group controlId="formFileLg" className="mb-4">

  <Form.Label className="custom-label">
    Sélectionner un fichier ou dossier ZIP
  </Form.Label>

  <div className="custom-file-upload">

    <label htmlFor="fileUpload" className="file-upload-label">

      <div className="file-upload-content">

        <div className="file-icon-container">
          <i className="fas fa-cloud-upload-alt"></i>
        </div>

        <div>
          <h5 className="file-title">
            Glissez votre fichier ici
          </h5>

          <p className="file-subtitle">
            ou cliquez pour sélectionner un fichier ZIP
          </p>
        </div>

      </div>

    </label>

    <Form.Control
      id="fileUpload"
      type="file"
      size="lg"
      className="d-none"
    />

  </div>

</Form.Group>

        <div className="scan-button-container">
          <ButtonScan nom="Lancer scan DevSecOps" />
        </div>

      </div>

    </div>
  );
}

export default DevSecOps;