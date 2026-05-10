import React from 'react'
import "../style/header.css"

function Header() {

  return (

    <nav className="navbar navbar-expand navbar-light topbar custom-topbar shadow-sm mb-4">

      {/* LEFT SIDE */}
      <div className="d-flex align-items-center">

        <button
          id="sidebarToggleTop"
          className="btn mobile-toggle d-md-none rounded-circle mr-3"
        >
          <i className="fa fa-bars"></i>
        </button>

        {/* SEARCH */}
        <form className="d-none d-sm-inline-block navbar-search">

          <div className="input-group custom-search">

            <span className="search-icon">
              <i className="fas fa-search"></i>
            </span>

            <input
              type="text"
              className="form-control border-0"
              placeholder="Rechercher scan, projet, rapport..."
            />

            <button className="search-btn" type="button">
              Search
            </button>

          </div>

        </form>

      </div>

      {/* RIGHT SIDE */}
      <ul className="navbar-nav ml-auto align-items-center">

        {/* ALERTS */}
        <li className="nav-item mx-2">

          <div className="header-icon-box">

            <i className="fas fa-bell"></i>

            <span className="notification-badge">
              3
            </span>

          </div>

        </li>

        {/* MESSAGES */}
        <li className="nav-item mx-2">

          <div className="header-icon-box">

            <i className="fas fa-envelope"></i>

            <span className="notification-badge">
              7
            </span>

          </div>

        </li>

        {/* DIVIDER */}
        <div className="topbar-divider d-none d-sm-block"></div>

        {/* USER */}
        <li className="nav-item dropdown no-arrow">

          <a
            className="nav-link dropdown-toggle user-box"
            href="#"
          >

            <div className="user-info">

              <span className="user-name">
                Admin
              </span>

              <span className="user-role">
                Security Analyst
              </span>

            </div>

            <img
              className="img-profile rounded-circle"
              src="https://i.pravatar.cc/150?img=12"
              alt="profile"
            />

          </a>

        </li>

      </ul>

    </nav>

  )
}

export default Header