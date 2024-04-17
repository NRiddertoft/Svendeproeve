import React, { useState, useEffect } from "react";
import "./FrontPage.css";
import LoginForm from "./LoginForm";
import { useNavigate } from "react-router-dom";
import "./Global.css";

function FrontPage() {
  const [showLoginForm, setShowLoginForm] = useState(false);
  const [showFilters, setShowFilters] = useState(false);
  const [peopleData, setPeopleData] = useState([]);
  const [selectedFilters, setSelectedFilters] = useState({
    studios: [],
    titles: [],
    projects: [],
    expertise: [],
  });

  const navigate = useNavigate();

  useEffect(() => {
    fetch("/jsontestdata/peopleData.txt")
      .then((response) => response.json())
      .then((data) => setPeopleData(data))
      .catch((error) => console.error("Error fetching people data:", error));
  }, []);

  const handleToggleFilters = () => {
    setShowFilters(!showFilters);
  };

  const handleClearFilters = () => {
    setSelectedFilters({
      studios: [],
      titles: [],
      projects: [],
      expertise: [],
    });
  };

  const handleLoginSuccess = () => {
    navigate("/admin");
  };

  // Function to get unique values from data
  const getUniqueValues = (data, key) => {
    return Array.from(new Set(data.map((item) => item[key]))).sort();
  };

  // Function to get unique values for array type data
  const getUniqueValuesForArray = (data, key) => {
    const allValues = data.flatMap((item) => item[key]);
    return Array.from(new Set(allValues)).sort();
  };
  const toggleFilter = (category, value) => {
    setSelectedFilters((prev) => ({
      ...prev,
      [category]: prev[category].includes(value)
        ? prev[category].filter((item) => item !== value)
        : [...prev[category], value],
    }));
  };

  // Extracting unique filter values
  const studios = getUniqueValues(peopleData, "studios");
  const titles = getUniqueValues(peopleData, "jobTitle");
  const projects = getUniqueValuesForArray(peopleData, "projects");
  const expertise = getUniqueValuesForArray(peopleData, "expertise");

  return (
    <div className="front-page">
      <img src="/images/Logo.png" alt="AKQA Logo" className="logo" />
      <div className="login-form-container">
        <div className="login-area">
          <div
            className="login-text"
            onClick={() => setShowLoginForm(!showLoginForm)}
          >
            Admin login
          </div>
          {showLoginForm && <LoginForm onLoginSuccess={handleLoginSuccess} />}
        </div>
      </div>

      <div className="search-container">
        <span className="magnifying-glass">🔍</span>
        <input
          type="text"
          placeholder="Search for a colleague"
          className="search-input"
        />
      </div>

      <div className="filters-actions">
        <div className="filters-button" onClick={handleToggleFilters}>
          Filters
        </div>
        <div className="clear-filters-button" onClick={handleClearFilters}>
          Clear filters
        </div>
      </div>

      {showFilters && (
        <div className="filters-panels">
          {/* Studios */}
          <div className="filter-panel">
            <h3>Studios</h3>
            {studios.map((studio) => (
              <div
                key={studio}
                onClick={() => toggleFilter("studios", studio)}
                className={
                  selectedFilters.studios.includes(studio) ? "selected" : ""
                }
              >
                {studio}
              </div>
            ))}
          </div>
          {/* Titles */}
          <div className="filter-panel">
            <h3>Title</h3>
            {titles.map((title) => (
              <div
                key={title}
                onClick={() => toggleFilter("titles", title)}
                className={
                  selectedFilters.titles.includes(title) ? "selected" : ""
                }
              >
                {title}
              </div>
            ))}
          </div>
          {/* Projects */}
          <div className="filter-panel">
            <h3>Projects</h3>
            {projects.map((project) => (
              <div
                key={project}
                onClick={() => toggleFilter("projects", project)}
                className={
                  selectedFilters.projects.includes(project) ? "selected" : ""
                }
              >
                {project}
              </div>
            ))}
          </div>
          {/* Expertise */}
          <div className="filter-panel">
            <h3>Expertise</h3>
            {expertise.map((expert) => (
              <div
                key={expert}
                onClick={() => toggleFilter("expertise", expert)}
                className={
                  selectedFilters.expertise.includes(expert) ? "selected" : ""
                }
              >
                {expert}
              </div>
            ))}
          </div>
        </div>
      )}

      <div className="people-container">
        {peopleData.map((person, index) => (
          <div key={index} className="person">
            <img src={person.image} alt={person.name} />
            <h3>{person.name}</h3>
            <p>Studios: {person.studios}</p>
            <p>Title: {person.jobTitle}</p>
            <p>Projects: {person.projects.join(", ")}</p>
            <p>Expertise: {person.expertise.join(", ")}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default FrontPage;
