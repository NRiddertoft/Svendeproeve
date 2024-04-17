import React, { useState, useEffect } from "react";
import "./LoginForm.css";

function LoginForm({ onLoginSuccess }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [adminCredentials, setAdminCredentials] = useState({});

  useEffect(() => {
    fetch("/jsontestdata/adminMockLogin.txt")
      .then((response) => response.json())
      .then((data) => setAdminCredentials(data))
      .catch((error) =>
        console.error("Error fetching admin credentials:", error)
      );
  }, []);

  const handleSubmit = (event) => {
    event.preventDefault();
    if (
      username === adminCredentials.username &&
      password === adminCredentials.password
    ) {
      console.log("Login successful");
      onLoginSuccess();
    } else {
      console.log("Incorrect username or password");
    }
  };

  return (
    <div className="login-form-container">
      <form onSubmit={handleSubmit} className="login-form">
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        <button type="submit">Login</button>
      </form>
    </div>
  );
}

export default LoginForm;
