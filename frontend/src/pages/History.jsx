import React from 'react'
import "../style/table.css"

function History() {
  return (
    <div>
      <div className="container-fluid">

        <h1 className="h3 mb-2 text-gray-800">Historique</h1>
        <hr />
        <div className="card shadow mb-4">
          <div className="card-header py-3">
            <h6 className="m-0 font-weight-bold text-primary">DataTables Example</h6>
          </div>
          <div className="card-body">
            <div className="table-responsive">
              <table className="table table-bordered" id="dataTable" width="100%" cellSpacing="0">
                <thead>
                  <tr>
                    <th className="attriutes">Date Scan </th>
                    <th className="attriutes">Titre Projet</th>

                  </tr>
                </thead>

                <tbody>
                  <tr>
                    <td>Tiger Nixon</td>
                    <td>System Architect</td>

                  </tr>
                  <tr>
                    <td>Garrett Winters</td>
                    <td>Accountant</td>

                  </tr>
              
                </tbody>
              </table>
            </div>
          </div>
        </div>

      </div>
    </div>
  )
}

export default History