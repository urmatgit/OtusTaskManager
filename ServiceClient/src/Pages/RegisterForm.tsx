import { Button, Form, Input, message } from "antd";
import { keycloak } from "../Services/keycloak";

interface RegisterData {
  username: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  password: string;
  confirmPassword: string;
}

export const RegisterForm = () => {
  const [form] = Form.useForm();

  const onFinish = (values: RegisterData) => {
    // Keycloak не принимает пароль напрямую — переходим на его форму
    keycloak.register({
      loginHint: values.username,
      action: "register",
    });
  };

  const validatePassword = (_: any, value: string) => {
    if (!value || value === form.getFieldValue("password")) {
      return Promise.resolve();
    }
    return Promise.reject(new Error("Пароли не совпадают!"));
  };

  return (
    <Form form={form} layout="vertical" onFinish={onFinish}>
      <Form.Item label="Логин" name="username" rules={[{ required: true }]}>
        <Input />
      </Form.Item>

      <Form.Item label="Имя" name="firstName" rules={[{ required: true }]}>
        <Input />
      </Form.Item>

      <Form.Item label="Фамилия" name="lastName" rules={[{ required: true }]}>
        <Input />
      </Form.Item>

      <Form.Item
        label="Email"
        name="email"
        rules={[{ required: true, type: "email" }]}
      >
        <Input />
      </Form.Item>

      <Form.Item label="Телефон" name="phone" rules={[{ required: true }]}>
        <Input />
      </Form.Item>

      <Form.Item
        label="Пароль"
        name="password"
        rules={[{ required: true, min: 6 }]}
      >
        <Input.Password />
      </Form.Item>

      <Form.Item
        label="Подтвердите пароль"
        name="confirmPassword"
        rules={[{ required: true, validator: validatePassword }]}
      >
        <Input.Password />
      </Form.Item>

      <Form.Item>
        <Button type="primary" htmlType="submit">
          Зарегистрироваться
        </Button>
      </Form.Item>
    </Form>
  );
};
