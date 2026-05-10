
import React from 'react'
function Sidebar() {
  return (
    <section>
      <ul className="navbar-nav  sidebar sidebar-dark accordion" id="accordionSidebar">

        <a className="sidebar-brand d-flex align-items-center justify-content-center" href="/">

          <div className="sidebar-brand-icon rotate-n-15">
            <i className="fas fa-robot"></i>
          </div>

          <div className="sidebar-brand-text mx-3">
            CyberAI <sup>Pro</sup>
          </div>

        </a>

        <hr className="sidebar-divider my-0" />

        <li className="nav-item active">
          <a className="nav-link" href="index.html">
            <i className="fas fa-fw fa-tachometer-alt"></i>
            <span>Dashboard</span></a>
        </li>

        <hr className="sidebar-divider" />


        <div className="sidebar-heading">
          Scan
        </div>

        <li className="nav-item">
          <a className="nav-link " href="/UserGui/Pentesting" >
            <i className="fas fa-user-secret"></i>
            <span>Pentesting</span>
          </a>

        </li>

        <li className="nav-item">
          <a className="nav-link " href="/UserGui/DevSecOps" >
            <i className="fas fa-shield-alt"></i>
            <span>DevSecOps</span>
          </a>

        </li>

        <hr className="sidebar-divider" />

        <div className="sidebar-heading">
          Archive
        </div>

        <li className="nav-item">
          <a className="nav-link " href="/UserGui/Project" >
            <i className="fas fa-folder-open"></i>
            <span>Projets</span>
          </a>
        </li>

        <li className="nav-item">
          <a className="nav-link  " href="/UserGui/Report" >
            <i className="fas fa-file-alt"></i>
            <span>Rapports</span></a>
        </li>

        <li className="nav-item">
          <a className="nav-link " href="/UserGui/History" >
            <i className="fas fa-history"></i>
            <span>Historiques</span></a>
        </li>

        <hr className="sidebar-divider d-none d-md-block" />
      </ul>
    </section>
  )
}

export default Sidebar
