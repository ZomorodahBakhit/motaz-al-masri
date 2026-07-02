import React from 'react';
import {Link} from 'react-router-dom';
import './Navbar.scss';

const Navbar: React.FC = () => {
    return (
        <nav className="navbar">
            <div className="navbar-logo">
                <h2>
                    University System
                </h2>
            </div>
            <ul className="navbar-links">
                <li>
                    <Link to="/">
                        Students
                    </Link>
                </li>
                <li>
                    <Link to="/about">about us</Link>
                </li>
            </ul>
        </nav>
    );
};

export default Navbar;``

