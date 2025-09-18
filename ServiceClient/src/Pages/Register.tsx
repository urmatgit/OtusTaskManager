import { Button, Card, Form, Input, Select } from "antd";
import { useEffect } from "react";
import { useNavigate, useOutletContext } from "react-router-dom";

import { register } from "../Services/authService";
import { useNotification } from "../Components/NotificationContext";
import { isAuthenticated } from "../ProtectedRoute";
import { UserRole } from "../Models/user";
const { Option } = Select;
type OutletContext = {
  handleLogin: () => void;
};

const Register = () => {
  const navigate = useNavigate();
  const [form] = Form.useForm();
  const notificationApi = useNotification();
  const { handleLogin } = useOutletContext<OutletContext>() || {};
  useEffect(() => {
    //const user = getCurrentUser();

    if (isAuthenticated()) {
      navigate("/dashboard");
    }
  }, [navigate]);

  const onFinish = async (values: {
    firstName: string;
    lastName: string;
    username: string;
    email: string;
    phone: string;
    password: string;
    role: UserRole;
  }) => {
    try {
      await register({
        FirstName: values.firstName,
        LastName: values.lastName,
        Username: values.username,
        Email: values.email,
        Phone: values.phone,
        Password: values.password,
        Role: values.role,
      });
      if (handleLogin !== null) handleLogin();
      // Уведомление об успешной регистрации
      notificationApi.success({
        message: "Регистрация прошла успешно",
        description: "Ваш аккаунт был успешно создан!",
        placement: "topRight",
        duration: 4,
      });

      navigate("/dashboard");
    } catch (error) {
      // Ошибки обрабатываются в authService
      console.error("Ошибка регистрации:", error);
      notificationApi.error({
        message: error.name,
        description: error.message,
        placement: "topRight",
        duration: 4.5,
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
      <Card
        title="Создать аккаунт"
        style={{
          width: "100%",
          maxWidth: 500,
          boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
        }}
      >
        <Form
          form={form}
          name="register"
          initialValues={{ role: "User" }}
          onFinish={onFinish}
          autoComplete="off"
          layout="vertical"
          scrollToFirstError
        >
          <div style={{ display: "flex", gap: 16 }}>
            <Form.Item
              label="Имя"
              name="firstName"
              rules={[
                { required: true, message: "Пожалуйста, введите ваше имя!" },
                { max: 50, message: "Максимум 50 символов" },
              ]}
              style={{ flex: 1 }}
            >
              <Input size="large" placeholder="Иван" />
            </Form.Item>

            <Form.Item
              label="Фамилия"
              name="lastName"
              rules={[
                {
                  required: true,
                  message: "Пожалуйста, введите вашу фамилию!",
                },
                { max: 50, message: "Максимум 50 символов" },
              ]}
              style={{ flex: 1 }}
            >
              <Input size="large" placeholder="Иванов" />
            </Form.Item>
          </div>

          <Form.Item
            label="Логин"
            name="username"
            rules={[
              {
                required: true,
                message: "Пожалуйста, выберите имя пользователя!",
              },
              { min: 4, message: "Минимум 4 символа" },
              { max: 20, message: "Максимум 20 символов" },
            ]}
          >
            <Input size="large" placeholder="ivanov" />
          </Form.Item>

          <Form.Item
            label="Email"
            name="email"
            rules={[
              { required: true, message: "Пожалуйста, введите ваш email!" },
              {
                type: "email",
                message: "Пожалуйста, введите корректный email",
              },
            ]}
          >
            <Input size="large" placeholder="ivan@example.com" />
          </Form.Item>

          <Form.Item
            label="Номер телефона"
            name="phone"
            rules={[
              {
                required: true,
                message: "Пожалуйста, введите ваш номер телефона!",
              },
              {
                pattern: /^[0-9+\- ]+$/,
                message: "Некорректный номер телефона",
              },
            ]}
          >
            <Input size="large" placeholder="+7 999 123 4567" />
          </Form.Item>

          <Form.Item
            label="Пароль"
            name="password"
            rules={[
              { required: true, message: "Пожалуйста, введите пароль!" },
              { min: 8, message: "Минимум 8 символов" },
              {
                pattern: /^(?=.*[A-Z])(?=.*[!@#$%^&*])/,
                message:
                  "Пароль должен содержать хотя бы 1 заглавную букву и 1 специальный символ (!@#$%^&*)",
              },
            ]}
            hasFeedback
          >
            <Input.Password size="large" placeholder="••••••••" />
          </Form.Item>

          <Form.Item
            label="Подтвердите пароль"
            name="confirmPassword"
            dependencies={["password"]}
            hasFeedback
            rules={[
              { required: true, message: "Пожалуйста, подтвердите пароль!" },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue("password") === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject("Пароли не совпадают!");
                },
              }),
            ]}
          >
            <Input.Password size="large" placeholder="••••••••" />
          </Form.Item>

          <Form.Item
            label="Роль"
            name="role"
            rules={[{ required: true, message: "Пожалуйста, выберите роль!" }]}
          >
            <Select size="large">
              <Option value={UserRole.User}>Пользователь</Option>
              {/* <Option value={UserRole.Admin}>Администратор</Option>
              <Option value={UserRole.Moderator}>Редактор</Option> */}
            </Select>
          </Form.Item>

          <Form.Item>
            <Button
              type="primary"
              htmlType="submit"
              block
              size="large"
              style={{ marginTop: 16 }}
            >
              Зарегистрироваться
            </Button>
          </Form.Item>

          <div style={{ textAlign: "center", marginTop: 16 }}>
            Уже есть аккаунт?{" "}
            <Button
              type="link"
              onClick={() => navigate("/login")}
              style={{ padding: 0 }}
            >
              Войти
            </Button>
          </div>
        </Form>
      </Card>
    </div>
  );
};

export default Register;
