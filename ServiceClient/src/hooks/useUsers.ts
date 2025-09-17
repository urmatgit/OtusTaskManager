import { useState, useEffect } from 'react';
import type { User  } from '../Models/user';
import { UserRole } from "../Models/user";
import { apiService } from '../Services/userService';
import { message } from 'antd';

export const useUsers = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchUsers = async () => {
    setLoading(true);
    setError(null);
    try {
      const userList = await apiService.getUsers();
      setUsers(userList);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to fetch users');
      message.error('Failed to load users');
    } finally {
      setLoading(false);
    }
  };

  const changeUserRole = async (userId: number, newRole: UserRole) => {
    try {
      const updatedUser = await apiService.changeUserRole({ userId, newRole });
      setUsers(prev => prev.map(user => 
        user.id === userId ? updatedUser : user
      ));
      message.success('Role updated successfully');
      return true;
    } catch (err) {
      message.error('Failed to update role');
      return false;
    }
  };

  const deleteUser = async (userId: string) => {
    try {
      await apiService.deleteUser(userId);
      setUsers(prev => prev.filter(user => user.id !== userId));
      message.success('User deleted successfully');
    } catch (err) {
      message.error('Failed to delete user');
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  return {
    users,
    loading,
    error,
    fetchUsers,
    changeUserRole,
    deleteUser,
  };
};