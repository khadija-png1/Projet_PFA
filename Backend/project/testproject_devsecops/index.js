const express = require("express");
const app = express();

app.get("/user", (req, res) => {
  const id = req.query.id;

  // ❌ SQL Injection simulation
  const query = "SELECT * FROM users WHERE id = " + id;

  res.send("Query executed: " + query);
});

app.listen(3000);