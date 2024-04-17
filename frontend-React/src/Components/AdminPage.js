import React from "react";
import "./AdminPage.css";
import "./Global.css";
import { useNavigate } from "react-router-dom"; // Import useNavigate

function AdminPage() {
  const navigate = useNavigate(); // Initialize navigate function

  const handleGoBack = () => {
    navigate("/"); // Navigate to the front page
  };

  return (
    <div>
      <div className="front-page">
      <h1 >ADMIN LOGIN</h1>
      
      <button onClick={handleGoBack}>Go Back to Frontpage</button>{" "}
      {/* Add a button to go back */}
    </div>
    </div>
  );
}

export default AdminPage;
