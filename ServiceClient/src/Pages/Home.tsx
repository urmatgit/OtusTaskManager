import React from "react";
import { useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { notification } from "antd";
import { isAuthenticated } from "../PrivateRoute";
const Home: React.FC = () => {
  const navigate = useNavigate();
  const [api, contextHolder] = notification.useNotification();

  // Проверка авторизации при загрузке
  useEffect(() => {
    if (!isAuthenticated()) {
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
