import React from 'react'
import Header from '../components/Header'
import Sidebar from '../components/Sidebar'
import Footer from '../components/Footer'
import { Outlet } from 'react-router-dom'

import "../style/UserGui.css"

function UserGui() {
  return (
    <div className="layout">

      <div className="sidebar-container">
        <Sidebar />
      </div>

      <div className="main-content">

        <Header />

        
          <Outlet />

        <Footer />

      </div>

    </div>
  )
}

export default UserGui