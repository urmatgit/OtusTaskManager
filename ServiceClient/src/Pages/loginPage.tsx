import { Button, Card, Form, Input, message, Typography } from "antd";
import { Link, useNavigate } from "react-router-dom";
import { login, getCurrentUser } from "../Services/authService";
import { useEffect, useRef } from "react";
import { useOutletContext } from "react-router-dom";
import { isAuthenticated } from "../ProtectedRoute";
import { useNotification } from "../Components/NotificationContext";
const { Text, Title } = Typography;
type OutletContext = {
  handleLogin: () => void;
};
const LoginPage = () => {
  const navigate = useNavigate();
  const [form] = Form.useForm();
  const notification = useNotification();
  const { handleLogin } = useOutletContext<OutletContext>() || {};

  // Проверка авторизации при загрузке
  const prevAuth = useRef(isAuthenticated);
  useEffect(() => {
    //const user = getCurrentUser();

    if (isAuthenticated()) {
      if (prevAuth.current !== isAuthenticated)
        notification.info({
          message: "Вы уже авторизованы",
          description: "Перенаправляем в личный кабинет",
        });

      navigate("/dashboard");
    }
  }, [navigate]);

  const onFinish = async (values: { username: string; password: string }) => {
    try {
      await login(values);
      if (handleLogin !== null) handleLogin();

      notification.success({
        message: "Вход выполнен",
        description: "Добро пожаловать!",
      });

      //navigate("/dashboard");
    } catch (error) {
      console.log(error);
      notification.error({
        message: "Ошибка входа",
        description: "Неверный логин или пароль!",
      });
    }
  };

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minHeight: "100vh",
        padding: "20px",
      }}
    >
      <div>
        <Card
          bordered={false}
          style={{
            boxShadow: "0 6px 16px rgba(0, 0, 0, 0.08)",
            borderRadius: "12px",
            overflow: "hidden",
          }}
          bodyStyle={{
            padding: "40px",
          }}
        >
          <div style={{ textAlign: "center", marginBottom: "32px" }}>
            <Title level={3} style={{ marginBottom: "8px" }}>
              Вход в аккаунт
            </Title>
            <Text type="secondary">Введите ваши учетные данные</Text>
          </div>

          <Form
            form={form}
            layout="vertical"
            onFinish={onFinish}
            autoComplete="off"
          >
            <Form.Item
              label="Email или имя пользователя"
              name="username"
              rules={[
                { required: true, message: "Обязательное поле" },
                { min: 4, message: "Минимум 4 символа" },
              ]}
            >
              <Input
                size="large"
                placeholder="example@mail.ru"
                style={{ borderRadius: "6px" }}
              />
            </Form.Item>

            <Form.Item
              label="Пароль"
              name="password"
              rules={[
                { required: true, message: "Введите пароль" },
                { min: 6, message: "Минимум 6 символов" },
              ]}
            >
              <Input.Password
                size="large"
                placeholder="••••••••"
                style={{ borderRadius: "6px" }}
              />
            </Form.Item>

            <Form.Item style={{ marginBottom: "16px" }}>
              <Button
                type="primary"
                htmlType="submit"
                block
                size="large"
                style={{
                  height: "48px",
                  borderRadius: "6px",
                  fontWeight: 500,
                }}
              >
                Войти
              </Button>
            </Form.Item>

            <div
              style={{
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
                marginTop: "16px",
              }}
            >
              <Link to="/forgot-password">
                <Button type="link" style={{ paddingLeft: 0 }}>
                  Забыли пароль?
                </Button>
              </Link>

              <Text>
                Нет аккаунта?{" "}
                <Link to="/register">
                  <Button type="link" style={{ padding: 0 }}>
                    Зарегистрироваться
                  </Button>
                </Link>
              </Text>
            </div>
          </Form>
        </Card>
      </div>
    </div>
  );
};

export default LoginPage;
