import React, { useState } from 'react';
import '../styles/authorize.css'

interface User {
    username: string;
    password: string;
}

const AuthPage: React.FC = () => {
    const [user, setUser] = useState<User>({ username: '', password: '' });
    const [isLogin, setIsLogin] = useState<boolean>(true);
    const [message, setMessage] = useState<string>('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setUser({ ...user, [name]: value });
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        // Здесь можно добавить логику для аутентификации или регистрации
        if (isLogin) {
            setMessage(`Вход выполнен для пользователя: ${user.username}`);
        } else {
            setMessage(`Регистрация завершена для пользователя: ${user.username}`);
        }
        // Сброс полей
        setUser({ username: '', password: '' });
    };

    return (
        <div className="auth-container">
            <h1>{isLogin ? 'Вход' : 'Регистрация'}</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="username">Имя пользователя:</label>
                    <input
                        type="text"
                        id="username"
                        name="username"
                        value={user.username}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Пароль:</label>
                    <input
                        type="password"
                        id="password"
                        name="password"
                        value={user.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <button type="submit">{isLogin ? 'Войти' : 'Зарегистрироваться'}</button>
            </form>
            <p onClick={() => setIsLogin(!isLogin)} style={{ cursor: 'pointer' }}>
                {isLogin ? 'Нет аккаунта? Зарегистрируйтесь' : 'Уже есть аккаунт? Войдите'}
            </p>
            {message && <p>{message}</p>}
        </div>
    );
};

export default AuthPage;