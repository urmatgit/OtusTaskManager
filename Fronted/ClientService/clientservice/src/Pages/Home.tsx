import React from 'react';
import { Link } from 'react-router-dom';

const Home: React.FC = () => {
    return (
        <div className="auth-container">
            <h1>Домашняя страница</h1>
            <p>Добро пожаловать на сайт!</p>
            <p>
                <Link to="/dashboard">
                    <button>Перейти в Dashboard</button>
                </Link>
            </p>
        </div>
    );
};

export default Home;