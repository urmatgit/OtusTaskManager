// src/pages/RegisterPage.tsx
import { Button, Form, Input, message } from "antd";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import api from "../Services/client";

export const RegisterPage = () => {
  const [loading, setLoading] = useState(false);
  const [redirectTime, setRedirectTime] = useState(0);
  const navigate = useNavigate();

  //   useEffect(() => {
  //     if (redirectTime > 0) {
  //       const timer = setTimeout(() => setRedirectTime(redirectTime - 1), 1000);
  //       return () => clearTimeout(timer);
  //     } else if (redirectTime === 0 && redirectTime !== 3) {
  //       navigate("/login");
  //     }
  //   }, [redirectTime, navigate]);

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const response = await api.post("/register", values);
      message.success(response.data.message);
      setRedirectTime(3); // начать отсчёт до редиректа
    } catch (error: any) {
      const errorMsg = error.response?.data?.message || "Ошибка регистрации";
      message.error(errorMsg);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h2>Создать аккаунт</h2>
        <p style={{ color: "#888", marginBottom: "24px" }}>
          Заполните форму для регистрации
        </p>

        {redirectTime > 0 ? (
          <div style={{ marginBottom: "24px", color: "#1890ff" }}>
            Проверьте email. Перенаправление через {redirectTime}...
          </div>
        ) : (
          <Form layout="vertical" onFinish={onFinish} disabled={loading}>
            <Form.Item
              label="Логин"
              name="username"
              rules={[{ required: true, message: "Введите логин" }]}
            >
              <Input placeholder="user123" />
            </Form.Item>

            <Form.Item
              label="Имя"
              name="firstName"
              rules={[{ required: true, message: "Введите имя" }]}
            >
              <Input placeholder="Иван" />
            </Form.Item>

            <Form.Item
              label="Фамилия"
              name="secondName"
              rules={[{ required: true, message: "Введите фамилию" }]}
            >
              <Input placeholder="Иванов" />
            </Form.Item>

            <Form.Item
              label="Email"
              name="email"
              rules={[
                {
                  required: true,
                  type: "email",
                  message: "Введите корректный email",
                },
              ]}
            >
              <Input placeholder="email@example.com" />
            </Form.Item>

            <Form.Item
              label="Телефон"
              name="phone"
              rules={[{ required: true, message: "Введите телефон" }]}
            >
              <Input placeholder="+7 (999) 123-45-67" />
            </Form.Item>

            <Form.Item>
              <Button
                type="primary"
                htmlType="submit"
                loading={loading}
                block
                size="large"
              >
                Зарегистрироваться
              </Button>
            </Form.Item>
          </Form>
        )}
      </div>
    </div>
  );
};

const styles = {
  container: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    minHeight: "80vh",
  },
  card: {
    backgroundColor: "white",
    padding: "40px",
    borderRadius: "8px",
    boxShadow: "0 4px 12px rgba(0,0,0,0.1)",
    width: "100%",
    maxWidth: 480,
  },
};
