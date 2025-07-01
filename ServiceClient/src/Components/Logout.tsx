import {
  Button,
  Space,
  Avatar,
  Typography,
  Dropdown,
  type MenuProps,
  Modal,
} from "antd";
import { LogoutOutlined, UserOutlined } from "@ant-design/icons";

import { useNavigate } from "react-router-dom";
import { getCurrentUser, logout } from "../Services/authService";
import { useState } from "react";
import { useNotification } from "../Components/NotificationContext";
const { Text } = Typography;
export const LogoutButton = ({ onLogout }) => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [currentUser] = useState(getCurrentUser());
  const [modal, contextHolderModal] = Modal.useModal();
  const notificationApi = useNotification();
  const handleLogout = async () => {
    setLoading(true);
    try {
      await logout();
      await onLogout();
      notificationApi.success({
        message: "Выход выполнен",
        description: "Вы успешно вышли из системы",
        placement: "topRight",
      });
      navigate("/login");
    } catch (error) {
      console.log(error);
      notificationApi.error({
        message: "Ошибка выхода",
        description: "Не удалось завершить сеанс",
        placement: "topRight",
      });
    } finally {
      setLoading(false);
    }
  };

  const showLogoutConfirm = () => {
    modal.confirm({
      title: "Подтверждение выхода",
      content: "Вы действительно хотите выйти из системы?",
      okText: "Выйти",
      cancelText: "Отмена",
      okButtonProps: { danger: true },
      centered: true,
      onOk: handleLogout,
    });
  };

  const items: MenuProps["items"] = [
    {
      key: "profile",
      label: "Мой профиль",
      icon: <UserOutlined />,
      onClick: () => navigate("/profile"),
    },
    {
      type: "divider",
    },
    {
      key: "logout",
      label: "Выйти",
      icon: <LogoutOutlined />,
      danger: true,
      onClick: showLogoutConfirm,
    },
  ];

  return (
    <>
      <Dropdown menu={{ items }} trigger={["click"]} placement="bottomRight">
        <Space style={{ cursor: "pointer" }}>
          <Avatar
            size="default"
            icon={<UserOutlined />}
            src={currentUser?.avatar}
          />
          <Text>{currentUser?.username}</Text>
        </Space>
      </Dropdown>
      <div>{contextHolderModal}</div>
    </>
  );
};
