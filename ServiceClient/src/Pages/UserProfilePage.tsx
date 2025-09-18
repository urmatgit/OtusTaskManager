// components/UserProfilePage.tsx
import React, { useState } from "react";
import {
  Card,
  Descriptions,
  Avatar,
  Tag,
  Spin,
  Button,
  message,
  Form,
  Input,
  Upload,
  Space,
  Divider,
  Modal,
  Row,
  Col,
  Switch,
} from "antd";
import {
  UserOutlined,
  EditOutlined,
  SaveOutlined,
  CloseOutlined,
  CameraOutlined,
  MailOutlined,
  PhoneOutlined,
  CalendarOutlined,
  ReloadOutlined,
} from "@ant-design/icons";
import type { UploadProps } from "antd";
import type { UserProfile, UpdateProfileRequest } from "../Models/user";
import { UserRole, Status } from "../Models/user";

import { useUserProfile } from "../hooks/useUserProfile";

interface UserProfilePageProps {
  userId?: string;
}

export const UserProfilePage: React.FC<UserProfilePageProps> = ({ userId }) => {
  const {
    profile,
    loading,
    updating,
    uploading,
    updateProfile,
    uploadAvatar,
    resendConfirmationEmail,
  } = useUserProfile(userId);
  const [editing, setEditing] = useState(false);
  const [form] = Form.useForm();

  const handleEdit = () => {
    setEditing(true);
    if (profile) {
      form.setFieldsValue(profile);
    }
  };

  const handleCancel = () => {
    setEditing(false);
    form.resetFields();
  };

  const handleSave = async (values: UpdateProfileRequest) => {
    try {
      await updateProfile(values);
      setEditing(false);
    } catch (error) {
      // Ошибка уже обработана в хуке
    }
  };

  const uploadProps: UploadProps = {
    beforeUpload: (file) => {
      const isImage = file.type.startsWith("image/");
      const isLt2M = file.size / 1024 / 1024 < 2;

      if (!isImage) {
        message.error("Можно загружать только изображения!");
        return Upload.LIST_IMPROVE;
      }
      if (!isLt2M) {
        message.error("Изображение должно быть меньше 2MB!");
        return Upload.LIST_IMPROVE;
      }

      // Загружаем файл
      uploadAvatar(file);
      return false; // Отменяем автоматическую загрузку
    },
    showUploadList: false,
    accept: "image/*",
  };

  const getStatusColor = (status: Status) => {
    switch (status) {
      case Status.Active:
        return "green";
      case Status.Inactive:
        return "orange";

      default:
        return "default";
    }
  };

  const getRoleColor = (role: UserRole) => {
    switch (role) {
      case UserRole.Admin:
        return "red";
      case UserRole.Moderator:
        return "blue";
      case UserRole.User:
        return "green";
      default:
        return "default";
    }
  };

  if (loading && !profile) {
    return (
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "400px",
        }}
      >
        <Spin size="large" />
      </div>
    );
  }

  if (!profile) {
    return (
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "400px",
          color: "#ff4d4f",
        }}
      >
        Профиль не найден
      </div>
    );
  }

  return (
    <div style={{ padding: "24px", maxWidth: "1000px", margin: "0 auto" }}>
      <Card
        title={
          <span style={{ fontSize: "20px", fontWeight: "600" }}>
            <UserOutlined /> Профиль пользователя
          </span>
        }
        extra={
          !editing ? (
            <Button
              type="primary"
              icon={<EditOutlined />}
              onClick={handleEdit}
              size="middle"
            >
              Редактировать
            </Button>
          ) : null
        }
      >
        <Form
          form={form}
          layout="vertical"
          onFinish={handleSave}
          initialValues={profile}
        >
          <Row gutter={[24, 24]}>
            <Col xs={24} md={8}>
              <div style={{ textAlign: "center" }}>
                <Upload {...uploadProps}>
                  <Avatar
                    size={120}
                    src={profile.avator || undefined}
                    icon={!profile.avator && <UserOutlined />}
                    style={{
                      cursor: "pointer",
                      border: "3px solid #d9d9d9",
                      marginBottom: "16px",
                    }}
                  />
                  <div>
                    <Button
                      icon={<CameraOutlined />}
                      loading={uploading}
                      type="link"
                    >
                      Изменить аватар
                    </Button>
                  </div>
                </Upload>

                <div style={{ marginTop: "16px" }}>
                  <h3 style={{ margin: 0 }}>
                    {profile.firstName} {profile.lastName}
                  </h3>
                  <Space style={{ marginTop: "8px" }}>
                    <Tag color={getRoleColor(profile.role)}>
                      {UserRole[profile.role]}
                    </Tag>
                    <Tag color={getStatusColor(profile.status)}>
                      {Status[profile.status]}
                    </Tag>
                  </Space>
                </div>
              </div>
            </Col>

            <Col xs={24} md={16}>
              <Descriptions
                column={1}
                bordered
                size="middle"
                labelStyle={{
                  fontWeight: 600,
                  width: "140px",
                  backgroundColor: "#fafafa",
                }}
              >
                <Descriptions.Item label="ID">
                  <span style={{ fontFamily: "monospace", fontSize: "12px" }}>
                    {profile.id}
                  </span>
                </Descriptions.Item>

                <Descriptions.Item label="Логин">
                  {editing ? (
                    <Form.Item
                      name="UserName"
                      style={{ margin: 0 }}
                      rules={[{ required: true, message: "Введите логин" }]}
                    >
                      <Input
                        prefix={<UserOutlined />}
                        placeholder="Введите логин"
                      />
                    </Form.Item>
                  ) : (
                    <Space>
                      <UserOutlined style={{ color: "#1890ff" }} />
                      {profile.userName}
                    </Space>
                  )}
                </Descriptions.Item>

                <Descriptions.Item label="Email">
                  {editing ? (
                    <Form.Item
                      name="Email"
                      style={{ margin: 0 }}
                      rules={[
                        { required: true, message: "Введите email" },
                        { type: "email", message: "Неверный формат email" },
                      ]}
                    >
                      <Input
                        prefix={<MailOutlined />}
                        placeholder="Введите email"
                      />
                    </Form.Item>
                  ) : (
                    <Space>
                      <Row>
                        <Col>
                          <MailOutlined style={{ color: "#1890ff" }} />
                          <span>{profile.email}</span>
                          <Tag
                            color={profile.emailConfirmed ? "green" : "orange"}
                          >
                            {profile.emailConfirmed
                              ? "Подтвержден"
                              : "Не подтвержден"}
                          </Tag>
                        </Col>
                      </Row>
                      <Row>
                        <Col>
                          {!profile.emailConfirmed && !userId && (
                            <Button
                              type="link"
                              size="small"
                              icon={<ReloadOutlined />}
                              onClick={resendConfirmationEmail}
                            >
                              Отправить подтверждение
                            </Button>
                          )}
                        </Col>
                      </Row>
                    </Space>
                  )}
                </Descriptions.Item>

                <Descriptions.Item label="Телефон">
                  {editing ? (
                    <Form.Item name="Phone" style={{ margin: 0 }}>
                      <Input
                        prefix={<PhoneOutlined />}
                        placeholder="Введите телефон"
                      />
                    </Form.Item>
                  ) : (
                    <Space>
                      <PhoneOutlined style={{ color: "#1890ff" }} />
                      {profile.phone || "—"}
                    </Space>
                  )}
                </Descriptions.Item>

                <Descriptions.Item label="Имя">
                  {editing ? (
                    <Form.Item
                      name="FirstName"
                      style={{ margin: 0 }}
                      rules={[{ required: true, message: "Введите имя" }]}
                    >
                      <Input placeholder="Введите имя" />
                    </Form.Item>
                  ) : (
                    profile.firstName
                  )}
                </Descriptions.Item>

                <Descriptions.Item label="Фамилия">
                  {editing ? (
                    <Form.Item
                      name="LastName"
                      style={{ margin: 0 }}
                      rules={[{ required: true, message: "Введите фамилию" }]}
                    >
                      <Input placeholder="Введите фамилию" />
                    </Form.Item>
                  ) : (
                    profile.lastName
                  )}
                </Descriptions.Item>

                <Descriptions.Item label="Отчество">
                  {editing ? (
                    <Form.Item name="Patronymic" style={{ margin: 0 }}>
                      <Input placeholder="Введите отчество" />
                    </Form.Item>
                  ) : (
                    profile.patronymic || "—"
                  )}
                </Descriptions.Item>

                <Descriptions.Item label="Дата регистрации">
                  <Space>
                    <CalendarOutlined style={{ color: "#1890ff" }} />
                    {new Date(profile.dateReg).toLocaleDateString("ru-RU", {
                      year: "numeric",
                      month: "long",
                      day: "numeric",
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                  </Space>
                </Descriptions.Item>
              </Descriptions>

              {editing && (
                <div
                  style={{
                    marginTop: "24px",
                    textAlign: "right",
                  }}
                >
                  <Space>
                    <Button
                      onClick={handleCancel}
                      icon={<CloseOutlined />}
                      size="large"
                    >
                      Отмена
                    </Button>
                    <Button
                      type="primary"
                      htmlType="submit"
                      icon={<SaveOutlined />}
                      size="large"
                      loading={updating}
                    >
                      Сохранить
                    </Button>
                  </Space>
                </div>
              )}
            </Col>
          </Row>
        </Form>
      </Card>
    </div>
  );
};

export default UserProfilePage;
