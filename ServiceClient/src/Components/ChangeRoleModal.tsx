import React from "react";
import { Modal, Form, Select, message } from "antd";
import type { User } from "../Models/user";
import { UserRole } from "../Models/user";
const { Option } = Select;

interface ChangeRoleModalProps {
  visible: boolean;
  user: User | null;
  onCancel: () => void;
  onSuccess: () => void;
  onChangeRole: (userId: string, newRole: UserRole) => Promise<boolean>;
}

export const ChangeRoleModal: React.FC<ChangeRoleModalProps> = ({
  visible,
  user,
  onCancel,
  onSuccess,
  onChangeRole,
}) => {
  const [form] = Form.useForm();
  const [loading, setLoading] = React.useState(false);

  React.useEffect(() => {
    if (visible && user) {
      form.setFieldsValue({
        role: user.role,
        userName: user.firstName,
        currentRole: UserRole[user.role],
      });
    }
  }, [visible, user, form]);

  const handleSubmit = async (values: { role: UserRole }) => {
    if (!user) return;

    setLoading(true);
    try {
      const success = await onChangeRole(user.id, values.role);
      if (success) {
        message.success("Role updated successfully");
        onSuccess();
        onCancel();
      }
    } catch (error) {
      message.error("Failed to update role");
    } finally {
      setLoading(false);
    }
  };

  const handleCancel = () => {
    form.resetFields();
    onCancel();
  };

  return (
    <Modal
      title="Change User Role"
      open={visible}
      onCancel={handleCancel}
      onOk={() => form.submit()}
      confirmLoading={loading}
      okText="Update Role"
    >
      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Form.Item label="User" name="userName">
          <input
            type="text"
            disabled
            style={{
              width: "100%",
              padding: "8px",
              border: "1px solid #d9d9d9",
              borderRadius: "6px",
              backgroundColor: "#f5f5f5",
            }}
          />
        </Form.Item>

        <Form.Item label="Current Role" name="currentRole">
          <input
            type="text"
            disabled
            style={{
              width: "100%",
              padding: "8px",
              border: "1px solid #d9d9d9",
              borderRadius: "6px",
              backgroundColor: "#f5f5f5",
            }}
          />
        </Form.Item>

        <Form.Item
          label="New Role"
          name="role"
          rules={[{ required: true, message: "Please select a role" }]}
        >
          <Select placeholder="Select new role">
            <Option value={UserRole.User}>User</Option>
            <Option value={UserRole.Admin}>Admin</Option>
            <Option value={UserRole.Owner}>Owner</Option>
            <Option value={UserRole.Editor}>Editor</Option>
          </Select>
        </Form.Item>
      </Form>
    </Modal>
  );
};
