// src/pages/LoginPage.tsx
import { Button, Form } from "antd";
import { useState } from "react";
import { keycloak } from "../Services/keycloak";

export const LoginPage = () => {
  const [loading, setLoading] = useState(false);

  const handleLogin = () => {
    setLoading(true);
    keycloak.login().finally(() => setLoading(false));
  };

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h2>Вход в систему</h2>
        <p style={{ color: "#888", marginBottom: "24px" }}>
          Введите свои данные для входа
        </p>

        <Form layout="vertical" onFinish={handleLogin}>
          <Form.Item>
            <Button
              type="primary"
              htmlType="submit"
              loading={loading}
              block
              size="large"
            >
              Войти через Keycloak
            </Button>
          </Form.Item>
        </Form>
        <div style={{ textAlign: "center", marginTop: "16px" }}>
          <a href="/register">Нет аккаунта? Зарегистрироваться</a>
        </div>
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
