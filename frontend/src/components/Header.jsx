import React, { useEffect, useState } from 'react'
import "../style/header.css"
import { supabase } from "../supabaseClient";
import { useNavigate } from "react-router-dom";

function Header() {

  const navigate = useNavigate();

  const [profile, setProfile] = useState(null);

  useEffect(() => {

    const getUserData = async () => {

      const {
        data: { user },
        error
      } = await supabase.auth.getUser();

      if (error) {
        console.log(error);
        return;
      }

      if (!user) return;

      console.log("USER =", user);

      setProfile({
        nom: user.user_metadata.nom,
        prenom: user.user_metadata.prenom,
        email: user.email
      });

    };

    getUserData();

  }, []);

  const handleLogout = async () => {

    await supabase.auth.signOut();

    navigate("/auth/login");

  };

  return (

    <nav className="navbar navbar-expand custom-topbar">

      {/* LEFT */}
      <div className="topbar-left">

        <button className="mobile-toggle d-md-none">
          <i className="fa fa-bars"></i>
        </button>

        <div className="custom-search">

          <i className="fas fa-search search-icon"></i>

          <input
            type="text"
            placeholder="Search scan, project, report..."
          />

          <button>
            Search
          </button>

        </div>

      </div>

      {/* RIGHT */}
      <div className="topbar-right">

        {/* NOTIFICATION */}
        <div className="header-icon-box">

          <i className="fas fa-bell"></i>

          <span className="notification-badge">
            3
          </span>

        </div>

        {/* MESSAGE */}
        <div className="header-icon-box">

          <i className="fas fa-envelope"></i>

          <span className="notification-badge">
            7
          </span>

        </div>

        {/* PROFILE */}
        <div className="profile-container dropdown">

          <button
            className="profile-box dropdown-toggle"
            type="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >

            <div className="user-info">

              <span className="user-name">

                {
                  profile
                    ? `${profile.prenom} ${profile.nom}`
                    : "Loading..."
                }

              </span>

              <span className="user-role">
                {profile?.email}
              </span>

            </div>

            <img
              className="img-profile"
              src={`https://ui-avatars.com/api/?name=${encodeURIComponent(
                `${profile?.prenom || ""} ${profile?.nom || ""}`
              )}&background=4435a5&color=fff&length=2`}
              alt="profile"
            />

          </button>

          {/* DROPDOWN */}
          <div className="dropdown-menu dropdown-menu-end profile-dropdown">
            <button
              className="dropdown-item logout-btn"
              onClick={handleLogout}
            >
              <i className="fas fa-sign-out-alt"></i>

              Logout
            </button>

          </div>

        </div>

      </div>

    </nav>

  )
}

export default Header