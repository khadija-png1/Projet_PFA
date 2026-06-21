import { useEffect, useState } from "react";
import { supabase } from "../supabaseClient";

function Report() {
  const [reports, setReports] = useState([]);

  const fetchReports = async () => {
    const { data, error } = await supabase
      .from("report")
      .select("*")
      .order("id", { ascending: false });

    if (error) console.log(error);
    else setReports(data);
  };

  useEffect(() => {
    fetchReports();
    const interval = setInterval(fetchReports, 3000);
    return () => clearInterval(interval);
  }, []);

  return (
    <div style={{ background: "#455fa1", minHeight: "100vh", padding: 20, color: "white" }}>
      
      <h2 style={{ color: "#ffcc00" }}>
        📄 SECURITY REPORTS
      </h2>

      <table style={{
        width: "100%",
        marginTop: 20,
        background: "#111827",
        borderRadius: 10,
        border: "1px solid #1f2937"
      }}>
        
        <thead>
          <tr style={{ background: "#1f2937" }}>
            <th>TITLE</th>
            <th>TYPE</th>
            <th>SUMMARY</th>
            <th>PDF</th>
          </tr>
        </thead>

        <tbody>
          {reports.map((r) => (
            <tr key={r.id}>
              <td>{r.report_name}</td>
              <td>{r.report_type}</td>
              
              <td>{r.summary?.slice(0, 60)}...</td>

              <td>
                <a
                  href={r.report_path}
                  target="_blank"
                  style={{ color: "#00ffcc" }}
                >
                  OPEN PDF
                </a>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

    </div>
  );
}

export default Report;