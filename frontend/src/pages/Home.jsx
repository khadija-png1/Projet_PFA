import React from "react";
import { Link } from "react-router-dom";

function Home() {
  return (
    <section
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        backgroundColor: "#634b69",
      }}
    >
      <p style={{ color: "#fff" }}>hello</p>

      <Link to="/auth/login" style={{ color: "#fff", marginLeft: "20px" }}>
        Login
      </Link>
      <Link to="/auth/register" style={{ color: "#fff", marginLeft: "20px" }}>
        Register
      </Link>
    </section>
  );
}

export default Home;