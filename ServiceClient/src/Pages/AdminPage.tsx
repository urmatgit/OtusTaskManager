import React, { useState } from "react";
import { Card, Button, Space, message } from "antd";
import type { User, UserRole } from "../Models/user";
import { UserTable } from "../Components//UserTable";
import { ChangeRoleModal } from "../Components/ChangeRoleModal";
import { useUsers } from "../hooks/useUsers";
import { hasRole } from "../Services/authService";
import { ReloadOutlined } from "@ant-design/icons";

export const AdminPage: React.FC = () => {
  const { users, loading, fetchUsers, changeUserRole, deleteUser } = useUsers();

  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [modalVisible, setModalVisible] = useState(false);

  const handleEditRole = (user: User) => {
    setSelectedUser(user);
    setModalVisible(true);
  };

  const handleDeleteUser = async (userId: string) => {
    await deleteUser(userId);
  };

  const handleRoleChange = async (userId: string, newRole: UserRole) => {
    return await changeUserRole(userId, newRole);
  };

  const handleModalSuccess = () => {
    fetchUsers(); // Refresh the list
  };

  if (!hasRole("Admin")) {
    return (
      <Card>
        <div style={{ textAlign: "center", padding: "40px" }}>
          <h2>Access Denied</h2>
          <p>You don't have permission to access this page.</p>
        </div>
      </Card>
    );
  }

  return (
    <div style={{ padding: "24px" }}>
      <Card
        title="User Management"
        extra={
          <Space>
            <Button
              icon={<ReloadOutlined />}
              onClick={fetchUsers}
              loading={loading}
            >
              Refresh
            </Button>
          </Space>
        }
      >
        <UserTable
          users={users}
          loading={loading}
          onEditRole={handleEditRole}
          onDeleteUser={handleDeleteUser}
        />

        <ChangeRoleModal
          visible={modalVisible}
          user={selectedUser}
          onCancel={() => setModalVisible(false)}
          onSuccess={handleModalSuccess}
          onChangeRole={handleRoleChange}
        />
      </Card>
    </div>
  );
};
