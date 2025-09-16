import { Layout, Menu, theme, Typography, Space } from "antd";
import {
  HomeOutlined,
  FileTextOutlined,
  BarChartOutlined,
} from "@ant-design/icons";
import { Link, Outlet, useNavigate, useLocation } from "react-router-dom";
import { getCurrentUser, logout } from "./Services/authService";

import { useEffect, useState } from "react";
import { LogoutButton } from "./Components/Logout";

import "./styles/authorize.css";
const { Header, Content, Footer } = Layout;
const { Text } = Typography;

// Конфигурация маршрутов
const menuItems = [
  {
    key: "/",
    icon: <HomeOutlined />,
    label: "Главная",
  },
  {
    key: "/dashboard",
    icon: <FileTextOutlined />,
    label: "Dashboard",
  },
  {
    key: "/ProjectList",
    icon: <BarChartOutlined />,
    label: "ProjectList",
  },
  {
    key: "/Board",
    icon: <BarChartOutlined />,
    label: "KanbanBoard",
  },
];

export const App = () => {
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const navigate = useNavigate();
  const location = useLocation();
  const [currentUser, setCurrentUser] = useState(getCurrentUser());
  const [selectedKeys, setSelectedKeys] = useState([location.pathname]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setSelectedKeys([location.pathname]);
    updateUserInfo();
  }, [location]);
  const updateUserInfo = async () => {
    const user = getCurrentUser();
    setCurrentUser(user);
  };
  // Handle logout function in App component
  const handleLogout = async () => {
    await updateUserInfo();
  };
  const handleLogin = async () => {
    await updateUserInfo();
  };
  if (!currentUser) {
    return (
      <Content>
        <Outlet context={{ handleLogin }} />
      </Content>
    );
  }

  return (
    <Layout style={{ minHeight: "100vh", background: "none" }}>
      {/* Верхняя панель */}
      <Header
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          padding: "0 24px",
          background: colorBgContainer,
          boxShadow: "0 2px 8px rgba(0, 0, 0, 0.1)",
          position: "sticky",
          top: 0,
          zIndex: 1,
        }}
      >
        {/* Логотип и навигация */}
        <Space>
          <Link to="/">
            <Text strong style={{ fontSize: "18px" }}>
              <HomeOutlined /> Таск менеджер
            </Text>
          </Link>

          <Menu
            theme="light"
            mode="horizontal"
            selectedKeys={selectedKeys}
            items={menuItems}
            onClick={({ key }) => navigate(key)}
            style={{ minWidth: 500 }}
          />
        </Space>
        {/* Блок пользователя */}
        <div>
          <LogoutButton onLogout={handleLogout} />
        </div>
      </Header>

      {/* Основное содержимое */}
      <Content style={{ padding: "8px" }}>
        <div
          style={{
            padding: 24,
            minHeight: "calc(100vh - 64px - 70px)",
            background: colorBgContainer,
            borderRadius: 8,
          }}
        >
          <Outlet />
        </div>
      </Content>

      {/* Подвал */}
      <Footer style={{ textAlign: "center", padding: "0" }}>
        Таск менеджер ©{new Date().getFullYear()}
      </Footer>
    </Layout>
  );
};
