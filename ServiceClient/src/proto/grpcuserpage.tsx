// src/components/UserList.tsx
import React, { useEffect, useState } from "react";
import {
  Row,
  Col,
  Card,
  Input,
  Button,
  Avatar,
  List,
  Tag,
  Space,
  Typography,
  Alert,
  Spin,
  Empty,
  Descriptions,
  Modal,
  Divider,
} from "antd";
import {
  SearchOutlined,
  ReloadOutlined,
  UserOutlined,
  MailOutlined,
  IdcardOutlined,
  TeamOutlined,
} from "@ant-design/icons";
import { useUsers } from "../proto/useUsers";
import type { UserGRPC as User } from "../proto/userGen";

const { Title, Text } = Typography;
const { Search } = Input;

const UserList: React.FC = () => {
  const { users, loading, error, getUsers } = useUsers();
  const [searchTerm, setSearchTerm] = useState("");
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [userDetailModalVisible, setUserDetailModalVisible] = useState(false);

  useEffect(() => {
    getUsers();
  }, [getUsers]);

  const filteredUsers = users.filter(
    (user) =>
      user.displayName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      user.email.toLowerCase().includes(searchTerm.toLowerCase()) ||
      user.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      user.lastName.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleUserClick = (user: User) => {
    setSelectedUser(user);
    setUserDetailModalVisible(true);
  };

  const handleCloseModal = () => {
    setUserDetailModalVisible(false);
    setSelectedUser(null);
  };

  const getInitials = (firstName: string, lastName: string): string => {
    return `${firstName.charAt(0)}${lastName.charAt(0)}`.toUpperCase();
  };

  return (
    <div style={{ padding: "24px", background: "#f0f2f5", minHeight: "100vh" }}>
      <Row gutter={[16, 16]}>
        <Col span={24}>
          <Card>
            <Space direction="vertical" style={{ width: "100%" }} size="large">
              {/* Header */}
              <div
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                }}
              >
                <Space>
                  <TeamOutlined
                    style={{ fontSize: "24px", color: "#1890ff" }}
                  />
                  <Title level={2} style={{ margin: 0 }}>
                    User Management
                  </Title>
                </Space>
                <Button
                  type="primary"
                  icon={<ReloadOutlined />}
                  onClick={getUsers}
                  loading={loading}
                >
                  Refresh
                </Button>
              </div>

              {/* Search and Stats */}
              <Space style={{ width: "100%", justifyContent: "space-between" }}>
                <Search
                  placeholder="Search users by name or email..."
                  allowClear
                  style={{ width: 400 }}
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  onSearch={setSearchTerm}
                />
                <Space>
                  <Text type="secondary">
                    Showing {filteredUsers.length} of {users.length} users
                  </Text>
                </Space>
              </Space>

              {/* Error Alert */}
              {error && (
                <Alert
                  message="Error"
                  description={error}
                  type="error"
                  showIcon
                  closable
                  action={
                    <Button size="small" onClick={getUsers}>
                      Retry
                    </Button>
                  }
                />
              )}
            </Space>
          </Card>
        </Col>

        {/* Users List */}
        <Col span={24}>
          <Card
            title={
              <Space>
                <UserOutlined />
                <span>Users</span>
                <Tag color="blue">{filteredUsers.length}</Tag>
              </Space>
            }
            loading={loading && users.length === 0}
          >
            {filteredUsers.length === 0 ? (
              <Empty
                image={Empty.PRESENTED_IMAGE_SIMPLE}
                description={
                  searchTerm ? "No users match your search" : "No users found"
                }
              >
                <Button type="primary" onClick={getUsers}>
                  Refresh
                </Button>
              </Empty>
            ) : (
              <List
                dataSource={filteredUsers}
                renderItem={(user) => (
                  <List.Item
                    actions={[
                      <Button type="link" onClick={() => handleUserClick(user)}>
                        View Details
                      </Button>,
                    ]}
                  >
                    <List.Item.Meta
                      avatar={
                        <Avatar
                          size="large"
                          style={{
                            backgroundColor: "#1890ff",
                            fontSize: "14px",
                          }}
                        >
                          {getInitials(user.firstName, user.lastName)}
                        </Avatar>
                      }
                      title={
                        <Space>
                          <Text strong>{user.displayName}</Text>
                          <Tag color="green" icon={<MailOutlined />}>
                            {user.email}
                          </Tag>
                        </Space>
                      }
                      description={
                        <Space direction="vertical" size={0}>
                          <Text type="secondary">
                            {user.firstName} {user.lastName}
                          </Text>
                          <Text type="secondary" style={{ fontSize: "12px" }}>
                            ID: {user.userId.substring(0, 8)}...
                          </Text>
                        </Space>
                      }
                    />
                  </List.Item>
                )}
                pagination={{
                  pageSize: 10,
                  showSizeChanger: true,
                  showQuickJumper: true,
                  showTotal: (total, range) =>
                    `${range[0]}-${range[1]} of ${total} items`,
                }}
              />
            )}
          </Card>
        </Col>
      </Row>

      {/* User Details Modal */}
      <Modal
        title={
          <Space>
            <UserOutlined />
            User Details
          </Space>
        }
        open={userDetailModalVisible}
        onCancel={handleCloseModal}
        footer={[
          <Button key="close" onClick={handleCloseModal}>
            Close
          </Button>,
          <Button
            key="refresh"
            type="primary"
            onClick={getUsers}
            loading={loading}
          >
            Refresh Data
          </Button>,
        ]}
        width={700}
      >
        {selectedUser && (
          <Space direction="vertical" style={{ width: "100%" }} size="large">
            {/* User Header */}
            <Card>
              <Space size="large">
                <Avatar
                  size={64}
                  style={{
                    backgroundColor: "#1890ff",
                    fontSize: "20px",
                  }}
                >
                  {getInitials(selectedUser.firstName, selectedUser.lastName)}
                </Avatar>
                <Space direction="vertical" size={0}>
                  <Title level={3} style={{ margin: 0 }}>
                    {selectedUser.displayName}
                  </Title>
                  <Text type="secondary">{selectedUser.email}</Text>
                </Space>
              </Space>
            </Card>

            {/* User Details */}
            <Card title="Personal Information">
              <Descriptions column={2} bordered>
                <Descriptions.Item label="First Name" span={1}>
                  {selectedUser.firstName}
                </Descriptions.Item>
                <Descriptions.Item label="Last Name" span={1}>
                  {selectedUser.lastName}
                </Descriptions.Item>
                <Descriptions.Item label="Display Name" span={1}>
                  {selectedUser.displayName}
                </Descriptions.Item>
                <Descriptions.Item label="Email" span={1}>
                  {selectedUser.email}
                </Descriptions.Item>
                <Descriptions.Item label="User ID" span={2}>
                  <Text code>{selectedUser.userId}</Text>
                </Descriptions.Item>
              </Descriptions>
            </Card>
          </Space>
        )}
      </Modal>

      {/* Loading Overlay */}
      {loading && users.length > 0 && (
        <div
          style={{
            position: "fixed",
            top: 0,
            left: 0,
            right: 0,
            bottom: 0,
            background: "rgba(0,0,0,0.1)",
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            zIndex: 1000,
          }}
        >
          <Card style={{ textAlign: "center" }}>
            <Spin size="large" />
            <div style={{ marginTop: 16 }}>
              <Text>Updating users...</Text>
            </div>
          </Card>
        </div>
      )}
    </div>
  );
};

export default UserList;
