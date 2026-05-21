import React, { useState } from 'react'
import "../../style/Register.css"
import { Link, useNavigate } from "react-router-dom";
import auth_image from "../../assets/image/image_auth.jpg"
import { supabase } from "../../supabaseClient";

export default function Register() {

  const navigate = useNavigate();

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirm: ""
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    if (form.password !== form.confirm) {
      alert("Passwords not match");
      return;
    }

    const { error } = await supabase.auth.signUp({
      email: form.email,
      password: form.password,

      options: {
        data: {
          nom: form.firstName,
          prenom: form.lastName,
        }
      }

    });

    if (error) {
      alert(error.message);
    } else {
      alert("Account created!");
      navigate("/auth/login");
    }
  };

  return (
    <section className="container-login">
      <div className="container">

        <div className="card shadow-lg my-5 form_design">
          <div className="card-body p-0">
            <div className="row">

              <div className="col-lg-5 d-none d-lg-block">
                <img src={auth_image} alt="" className='image' />
              </div>

              <div className="col-lg-7">
                <div className="p-5">

                  <h1 className="h4 text-center mb-4">Create Account</h1>

                  <form onSubmit={handleRegister}>

                    <input name="firstName" onChange={handleChange} className="form-control mb-2" placeholder="First Name" />

                    <input name="lastName" onChange={handleChange} className="form-control mb-2" placeholder="Last Name" />

                    <input name="email" onChange={handleChange} className="form-control mb-2" placeholder="Email" />

                    <input name="password" type="password" onChange={handleChange} className="form-control mb-2" placeholder="Password" />

                    <input name="confirm" type="password" onChange={handleChange} className="form-control mb-3" placeholder="Confirm Password" />

                    <button className="btn btn-primary w-100">
                      Register
                    </button>

                  </form>

                  <Link to="/auth/login">Already have account?</Link>

                </div>
              </div>

            </div>
          </div>
        </div>

      </div>
    </section>
  )
}