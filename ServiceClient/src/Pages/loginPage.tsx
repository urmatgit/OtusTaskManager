import { Button, Card, Form, Input, Typography } from "antd";
import { Link, useNavigate } from "react-router-dom";
import { login, getCurrentUser } from "../Services/authService";
import { useEffect } from "react";
import { Notification } from "../Components/Notification"; // Ваш компонент уведомлений

const { Text, Title } = Typography;

const LoginPage = () => {
  const navigate = useNavigate();
  const [form] = Form.useForm();

  // Проверка авторизации при загрузке
  useEffect(() => {
    const user = getCurrentUser();
    if (user) {
      navigate("/dashboard");
      Notification.info(
        "Вы уже авторизованы",
        "Перенаправляем в личный кабинет"
      );
    }
  }, [navigate]);

  const onFinish = async (values: { username: string; password: string }) => {
    try {
      await login(values);
      navigate("/dashboard");
      Notification.success("Вход выполнен", "Добро пожаловать!");
    } catch (error) {
      console.log(error);
      Notification.error("Ошибка входа", "Неверные учетные данные");
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
