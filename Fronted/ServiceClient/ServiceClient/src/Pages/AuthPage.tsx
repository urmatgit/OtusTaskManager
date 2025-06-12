import React, { useState } from 'react';
import '../styles/authorize.css';
import { Link } from 'react-router-dom';

const apiBaseUrl = 'http://localhost:5250'; // URL вашего Web API

const LoginPage: React.FC = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [userName, setUserName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [accessToken, setAccessToken] = useState('');
    const [isLogin, setIsLogin] = useState<boolean>(true);

    const handleLogin = async () => {
        try {
            const response = await fetch(`${apiBaseUrl}/login`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    email,
                    password
                })
            });

            if (response.ok) {
                const data = await response.json();
                setUserName(data.username);
                setIsLoggedIn(true);
                setAccessToken(data.access_token);
                sessionStorage.setItem('accessToken', data.access_token);
            } else {
                console.log('Ошибка входа:', response.status);
            }
        } catch (error) {
            console.error('Ошибка:', error);
        }
    };

    const handleGetData = async () => {
        try {
            const token = sessionStorage.getItem('accessToken');
            const response = await fetch(`${apiBaseUrl}/data`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const data = await response.json();
                alert(data.message);
            } else {
                console.log('Ошибка получения данных:', response.status);
            }
        } catch (error) {
            console.error('Ошибка:', error);
        }
    };

    const handleLogout = () => {
        setIsLoggedIn(false);
        setUserName('');
        sessionStorage.removeItem('accessToken');
    };

    return (
        <div className="auth-container">
            {isLoggedIn ? (
                <div className="userInfo">
                    <p>Добро пожаловать {userName}!</p>
                    <button onClick={handleLogout}>Выйти</button>
                    <p>
                        <Link to="/Board">
                            <button>Перейти в KanbanBoard</button>
                        </Link>
                    </p>
                </div>
            ) : (
                <div className="loginForm">
                    <h1>{isLogin ? 'Вход' : 'Регистрация'}</h1>
                    <div>
                        <label>
                            Введите email:
                            <input
                                type="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                            />
                        </label>
                    </div>
                    <div>
                        <label>
                            Введите пароль:
                            <input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </label>
                    </div>
                        <button onClick={handleLogin}>{isLogin ? 'Войти' : 'Зарегистрироваться'}</button>
                        <p onClick={() => setIsLogin(!isLogin)} style={{ cursor: 'pointer' }}>
                            {isLogin ? 'Нет аккаунта? Зарегистрируйтесь' : 'Уже есть аккаунт? Войдите'}
                        </p>
                </div>
            )}
            <button onClick={handleGetData}>Получить данные</button>
        </div>
    );
};

export default LoginPage;