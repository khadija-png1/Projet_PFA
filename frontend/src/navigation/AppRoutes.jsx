import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Home from "../pages/Home";
import Login from "../pages/Auth/Login";
import Register from "../pages/Auth/Register"
import UserGui from "../pages/UserGui";
import Project from "../pages/Project";
import Report from "../pages/Report";
import History from "../pages/History";
import DevSecOps from "../pages/DevSecOps";
import Pentesting from "../pages/Pentesting";
import ProtectedRoute from "../components/ProtectedRoute";
function AppRoutes() {
  return (
    <Router>
      <Routes>

        <Route path="/" element={<Home />} />
        <Route path="/auth/login" element={<Login />} />
        <Route path="/auth/register" element={<Register />} />

        <Route
          path="/UserGui"
          element={
            <ProtectedRoute>
              <UserGui />
            </ProtectedRoute>
          }
        >
          <Route index element={<div>Welcome Dashboard</div>} />

          <Route path="Project" element={<Project />} />
          <Route path="Report" element={<Report />} />
          <Route path="History" element={<History />} />
          <Route path="DevSecOps" element={<DevSecOps />} />
          <Route path="Pentesting" element={<Pentesting />} />
        </Route>

      </Routes>
    </Router >
  );
}

export default AppRoutes;