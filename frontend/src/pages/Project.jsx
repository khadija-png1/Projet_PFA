import React from 'react'

import "../style/table.css"

function Project() {
  return (
    <div className="container-fluid">

        <h1 className="h3 mb-2 text-gray-800">Projets</h1>
        <hr />
        <div className="card shadow mb-4">
          <div className="card-header py-3">
            <h6 className="m-0 font-weight-bold text-primary">DataTables Example</h6>
          </div>
          <div className="card-body">
            <div className="table-responsive">
              <table className="table table-bordered" id="dataTable" width="100%" cellSpacing="0">
                <thead >
                  <tr >
                    <th className="attriutes">Type Scan</th>
                    <th className="attriutes">Titre Projet</th>
                    <th className="attriutes">Description</th>
                    <th className="attriutes">Rapport</th>
                    <th className="attriutes">Nombre de Vulnerabilité</th>
                    <th className="attriutes">Date Scan</th>
                  </tr>
                </thead>

                <tbody>
                  <tr>
                    <td>Tiger Nixon</td>
                    <td>System Architect</td>
                    <td>Edinburgh</td>
                    <td>61</td>
                    <td>2011/04/25</td>
                    <td>$320,800</td>
                  </tr>
                  <tr>
                    <td>Garrett Winters</td>
                    <td>Accountant</td>
                    <td>Tokyo</td>
                    <td>63</td>
                    <td>2011/07/25</td>
                    <td>$170,750</td>
                  </tr>
       
                </tbody>
              </table>
            </div>
          </div>
        </div>

      </div>
  )
}

export default Project