import React from 'react'
import "../style/buttonScan.css"

function ButtonScan({nom}) {
  return (
    <div className="container-fluid d-flex justify-content-center align-items-center py-5">

      <button className="scan-btn">
        
        <span className="scan-icon">
          <i className="fas fa-shield-alt"></i>
        </span>

        <span className="scan-text">
          {nom}
        </span>

      </button>

    </div>
  )
}

export default ButtonScan