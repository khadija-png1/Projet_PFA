import { useEffect, useState } from "react";
import { supabase } from "../supabaseClient";

function Project() {
  const [projects, setProjects] = useState([]);

  const fetchProjects = async () => {
    const { data, error } = await supabase
      .from("project")
      .select("*")
      .order("id", { ascending: false });

    if (error) {
      console.log(error);
    } else {
      setProjects(data);
    }
  };

  useEffect(() => {
    fetchProjects();

    const interval = setInterval(fetchProjects, 3000);

    return () => clearInterval(interval);
  }, []);

  return (
    <div style={{ background: "#0a0f1c", minHeight: "100vh", padding: 20, color: "white" }}>
      
      <h2 style={{ color: "#00ffcc", textShadow: "0 0 10px #00ffcc" }}>
        🛡️ DEVSECOPS PROJECTS
      </h2>

      <table style={{
        width: "100%",
        marginTop: 20,
        background: "#111827",
        borderRadius: 10,
        overflow: "hidden",
        border: "1px solid #1f2937"
      }}>
        
        <thead>
          <tr style={{ background: "#1f2937" }}>
            <th>TYPE</th>
            <th>TITLE</th>
            <th>DESCRIPTION</th>
            <th>SCORE</th>
            <th>DATE</th>
          </tr>
        </thead>

        <tbody>
          {projects.length === 0 ? (
            <tr>
              <td colSpan="6" style={{ textAlign: "center", padding: 20 }}>
                Loading security scans...
              </td>
            </tr>
          ) : (
            projects.map((p) => (
              <tr key={p.id} style={{ borderBottom: "1px solid #2d3748" }}>
                
                <td>
                  <span style={{
                    padding: "4px 8px",
                    borderRadius: 5,
                    background: p.scan_type === "SAST" ? "#00ff88" : "#ff0055",
                    color: "black",
                    fontWeight: "bold"
                  }}>
                    {p.scan_type}
                  </span>
                </td>

                <td>{p.title}</td>
                <td>{p.description}</td>

             

                <td style={{ color: "#00ffcc" }}>
                  {p.severity_score}
                </td>

                <td>
                  {new Date(p.created_at).toLocaleString()}
                </td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
}

export default Project;