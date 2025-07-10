import React from "react";
import { useAuth } from "../Components/AuthContext";
import { Button, Card, Layout, Typography, Space } from "antd";
import { LoginOutlined } from "@ant-design/icons";
import "../Styles/LoginPage.css";

const { Content } = Layout;
const { Title, Text } = Typography;

const LoginPage1: React.FC = () => {
  const { login } = useAuth();

  return (
    <Layout style={{ minHeight: "100vh", background: "#f0f2f5" }}>
      <Content
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Card style={{ width: 420, textAlign: "center" }}>
          <Space direction="vertical" size="large">
            <img src="/logo.svg" alt="Logo" style={{ height: "64px" }} />
            <Title level={3}>Welcome to MyApp</Title>
            <Text type="secondary">Please sign in to continue</Text>
            <Button
              type="primary"
              size="large"
              icon={<LoginOutlined />}
              onClick={login}
              block
            >
              Sign In with Keycloak
            </Button>
          </Space>
        </Card>
      </Content>
    </Layout>
  );
};

export default LoginPage1;
