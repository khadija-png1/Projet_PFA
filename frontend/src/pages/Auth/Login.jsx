import React, { useState } from 'react'
import "../../style/Login.css"
import { Link, useNavigate } from "react-router-dom";
import auth_image from "../../assets/image/image_auth.jpg"
import { supabase } from "../../supabaseClient";

export default function Login() {

  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async (e) => {
    e.preventDefault();

    const { error } = await supabase.auth.signInWithPassword({
      email,
      password
    });

    if (error) {
      alert(error.message);
    } else {
      navigate("/UserGui");
    }
  };

  return (
    <section className="container-login">

      <div className="container">

        <div className="card shadow-lg my-5">

          <div className="row">

            <div className="col-lg-6 d-none d-lg-block">
              <img src={auth_image} className="image" />
            </div>

            <div className="col-lg-6">
              <div className="p-5">

                <h1>Welcome</h1>

                <form onSubmit={handleLogin}>

                  <input
                    className="form-control mb-2"
                    placeholder="Email"
                    onChange={(e) => setEmail(e.target.value)}
                  />

                  <input
                    type="password"
                    className="form-control mb-3"
                    placeholder="Password"
                    onChange={(e) => setPassword(e.target.value)}
                  />

                  <button className="btn btn-success w-100">
                    Login
                  </button>

                </form>

                <Link to="/auth/register">Register</Link>

              </div>
            </div>

          </div>

        </div>

      </div>

    </section>
  )
}