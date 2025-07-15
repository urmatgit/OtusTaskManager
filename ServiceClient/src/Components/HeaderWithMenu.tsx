import React from "react";
import { useAuth } from "./AuthContext";
import { useNavigate, useLocation } from "react-router-dom";
import {
  Layout,
  Menu,
  Avatar,
  Dropdown,
  Space,
  Typography,
  Button,
  theme,
  Badge,
  Divider,
} from "antd";
import {
  UserOutlined,
  LogoutOutlined,
  DashboardOutlined,
  TeamOutlined,
  SettingOutlined,
  BellOutlined,
  LoginOutlined,
} from "@ant-design/icons";

const { Header } = Layout;
const { Text } = Typography;

const HeaderWithMenu: React.FC = () => {
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const navigate = useNavigate();
  const location = useLocation();
  const { isAuthenticated, isLoading, profile, logout, login } = useAuth();

  const menuItems = [
    {
      key: "dashboard",
      icon: <DashboardOutlined />,
      label: "Dashboard",
      onClick: () => navigate("/dashboard"),
    },
    {
      key: "kanban",
      icon: <TeamOutlined />,
      label: "kanban",
      onClick: () => navigate("/kanban"),
    },
    {
      key: "projects",
      icon: <SettingOutlined />,
      label: "Projects",
      onClick: () => navigate("/projects"),
    },
    {
      key: "settings",
      icon: <SettingOutlined />,
      label: "Settings",
      onClick: () => navigate("/settings"),
    },
  ];

  const profileMenu = (
    <Menu>
      <Menu.ItemGroup title="User Profile">
        <Menu.Item key="profile:1" style={{ cursor: "default" }}>
          <Space direction="vertical">
            <Text strong>{profile.name}</Text>
            <Text type="secondary">{profile.email}</Text>
          </Space>
        </Menu.Item>
      </Menu.ItemGroup>
      <Divider style={{ margin: "4px 0" }} />
      <Menu.Item
        key="profile:logout"
        icon={<LogoutOutlined />}
        onClick={logout}
        danger
      >
        Logout
      </Menu.Item>
    </Menu>
  );

  return (
    <Header
      style={{
        position: "sticky",
        top: 0,
        zIndex: 1,
        width: "100%",
        display: "flex",
        alignItems: "center",
        background: colorBgContainer,
        padding: "0 24px",
        boxShadow: "0 1px 4px 0 rgba(0, 21, 41, 0.12)",
      }}
    >
      <div
        style={{ display: "flex", alignItems: "center", marginRight: "24px" }}
      >
        <img
          src="/logo.svg"
          alt="Logo"
          style={{ height: "32px", marginRight: "8px" }}
        />
        <Text strong style={{ fontSize: "18px" }}>
          MyApp
        </Text>
      </div>

      {isAuthenticated && (
        <Menu
          theme="light"
          mode="horizontal"
          selectedKeys={[location.pathname.split("/")[1] || "dashboard"]}
          items={menuItems}
          style={{ flex: 1, minWidth: 0 }}
        />
      )}

      <Space size="middle" style={{ marginLeft: "auto" }}>
        {isAuthenticated && (
          <>
            <Badge count={5} size="small">
              <Button
                type="text"
                shape="circle"
                icon={<BellOutlined />}
                onClick={() => navigate("/notifications")}
              />
            </Badge>

            <Dropdown overlay={profileMenu} placement="bottomRight">
              <Space style={{ cursor: "pointer" }}>
                <Avatar
                  size="default"
                  icon={<UserOutlined />}
                  style={{ backgroundColor: "#1890ff" }}
                />
                <Text strong>{profile.name}</Text>
              </Space>
            </Dropdown>
          </>
        )}

        {!isAuthenticated && !isLoading && (
          <Button type="primary" icon={<LoginOutlined />} onClick={login}>
            Sign In
          </Button>
        )}

        {isLoading && (
          <Avatar
            size="default"
            icon={<UserOutlined />}
            style={{ backgroundColor: "#f0f0f0" }}
          />
        )}
      </Space>
    </Header>
  );
};

export default HeaderWithMenu;
