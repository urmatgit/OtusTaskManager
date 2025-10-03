// src/hooks/useUsers.ts
import { useState, useCallback } from 'react';
import { userServiceClient } from '../proto/UserServiceClient';
import type { UserGRPC } from './userGen';

export const useUsers = () => {
  const [users, setUsers] = useState<UserGRPC[]>([]);
  const [selectedUser, setSelectedUser] = useState<UserGRPC | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const getUsers = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const usersList = await userServiceClient.getUsers();
      setUsers(usersList);
      return usersList;
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to fetch users';
      setError(errorMessage);
      throw err;
    } finally {
      setLoading(false);
    }
  }, []);

  const getUser = useCallback(async (userId: string) => {
    setLoading(true);
    setError(null);
    try {
      const user = await userServiceClient.getUser(userId);
      setSelectedUser(user);
      return user;
    } catch (err) {
      const errorMessage = err instanceof Error ? err.message : 'Failed to fetch user';
      setError(errorMessage);
      throw err;
    } finally {
      setLoading(false);
    }
  }, []);

  const clearError = useCallback(() => {
    setError(null);
  }, []);

  const clearSelectedUser = useCallback(() => {
    setSelectedUser(null);
  }, []);

  return {
    users,
    selectedUser,
    loading,
    error,
    getUsers,
    getUser,
    clearError,
    clearSelectedUser,
  };
};