import React from "react";
import logo from './logo.svg';
import './App.css';
import {
  BrowserRouter as Router,
  Routes,
  Route,
  NavLink,
  Link,
} from "react-router-dom";
import UserListPage from "./ui/user/UserListPage";
import RoleListPage from "./ui/role/RoleListPage";
import Home from "./ui/Home";
import UserCreate from "./ui/user/UserCreate";
import UserEdit from "./ui/user/UserEdit";

function App() {
  return (
        <Router>
            <nav className="navbar navbar-light bg-green-light">
                <Link className="navbar-brand" to="/">Navbar</Link>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                  <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                  <ul className="navbar-nav">
                    <li className="nav-item active">
                      <Link className="nav-link" to="/">Home</Link>
                    </li>
                    <li className="nav-item">
                      <Link className="nav-link" to="/users">Users</Link>
                    </li>
                    <li className="nav-item">
                      <Link className="nav-link" to="/roles">Roles</Link>
                    </li>
                  </ul>
                </div>
            </nav>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/users" element={<UserListPage />} />
                <Route path="/users/new" element={<UserCreate />} />
                <Route path="/users/edit/:id" element={<UserEdit />} />
                <Route
                    path="/roles"
                    element={<RoleListPage />}
                />
            </Routes>
        </Router>
  );
}

export default App;
