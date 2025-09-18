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
      setError(err instanceof Error ? err.message : 'Не удалось найти пользователей');
      message.error('Не удалось найти пользователей');
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
      message.success('оль успешно обновлена');
      return true;
    } catch (err) {
      message.error('Не удалось обновить роль');
      return false;
    }
  };

  const deleteUser = async (userId: string) => {
    try {
      await apiService.deleteUser(userId);
      setUsers(prev => prev.filter(user => user.id !== userId));
      message.success('Пользователь успешно удален');
    } catch (err) {
      message.error('Не удалось удалить пользователя');
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