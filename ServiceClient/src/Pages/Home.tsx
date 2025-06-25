import React from "react";
import { useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { notification } from "antd";
import { login, getCurrentUser } from "../Services/authService";

const Home: React.FC = () => {
  const navigate = useNavigate();
  const [api, contextHolder] = notification.useNotification();

  // Проверка авторизации при загрузке
  useEffect(() => {
    const user = getCurrentUser();
    if (user == null) {
      navigate("/login");
      api.info({
        message: "Вы еще не уже авторизованы",
        description: "Перенаправляем на страницу авторизации",
        placement: "topRight",
      });
    }
  }, [navigate, api]);
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
