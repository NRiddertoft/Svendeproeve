import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import FrontPage from "./Components/FrontPage";
import AdminPage from "./Components/AdminPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<FrontPage />} />
        <Route path="/admin" element={<AdminPage />} />
        {/* Redirect is replaced by Navigate in v6 for redirection */}
        {/* If you need a catch-all route to redirect, you can use Navigate */}
        {/* <Route path="*" element={<Navigate to="/" replace />} /> */}
      </Routes>
    </Router>
  );
}

export default App;
