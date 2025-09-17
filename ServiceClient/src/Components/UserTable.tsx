import React from "react";

import { Table, Button, Space, Tag, Popconfirm } from "antd";
import type { User } from "../Models/user";
import { UserRole } from "../Models/user";
import { hasRole, getCurrentUser } from "../Services/authService";

interface UserTableProps {
  users: User[];
  loading: boolean;
  onEditRole: (user: User) => void;
  onDeleteUser: (userId: string) => void;
}

export const UserTable: React.FC<UserTableProps> = ({
  users,
  loading,
  onEditRole,
  onDeleteUser,
}) => {
  //const { user: currentUser, hasRole } = useAuth();

  const getRoleColor = (role: UserRole) => {
    switch (role) {
      case UserRole.Admin:
        return "red";
      case UserRole.Owner:
        return "purple";
      case UserRole.User:
        return "blue";
      default:
        return "default";
    }
  };

  const columns = [
    {
      title: "Username",
      dataIndex: "userName",
      key: "userName",
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
    },
    {
      title: "Role",
      dataIndex: "role",
      key: "role",
      render: (role: UserRole) => (
        <Tag color={getRoleColor(role)}>{UserRole[role]}</Tag>
      ),
    },
    {
      title: "Created At",
      dataIndex: "dateReg",
      key: "createdAt",
      render: (date: string) => new Date(date).toLocaleDateString(),
    },
    {
      title: "Actions",
      key: "actions",
      render: (_: any, record: User) => (
        <Space size="middle">
          <Button
            type="primary"
            onClick={() => onEditRole(record)}
            disabled={
              // Cannot edit own role or users with higher privileges
              !hasRole(UserRole.Admin)
            }
          >
            Изменить роль
          </Button>

          <Popconfirm
            title="Вы уверены, что хотите удалить этого пользователя?"
            onConfirm={() => onDeleteUser(record.id)}
            okText="Да"
            cancelText="Нет"
            disabled={!hasRole(UserRole.Admin)}
          >
            <Button
              type="primary"
              danger
              disabled={
                record.id === getCurrentUser()?.id && !hasRole(UserRole.Admin)
              }
            >
              Удалить
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <Table
      columns={columns}
      dataSource={users}
      loading={loading}
      rowKey="id"
      pagination={{ pageSize: 10 }}
      scroll={{ x: 800 }}
    />
  );
};
