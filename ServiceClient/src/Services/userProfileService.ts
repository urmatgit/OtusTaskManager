// services/userService.ts
import axios from 'axios';
import  type { UserProfile, UpdateProfileRequest } from '../Models/user';
import { getCurrentUser } from "./authService";
const API_BASE_URL =  'http://localhost:5191/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
});

// Добавляем интерцептор для авторизации
api.interceptors.request.use((config) => {
  const token = getCurrentUser()?.token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const userProfileService = {
  // Получить профиль текущего пользователя
  getMyProfile: async (): Promise<UserProfile> => {
    const config = {
              headers: {
                'Accept': 'application/json',
                'Content-Type':'application/json',
                'Authorization': `Bearer ${getCurrentUser()?.token}` // Assuming a Bearer token
              }
            }
    const response = await api.get('/user/profile',config);
    return response.data;
  },

  // Получить профиль по ID
  getProfileById: async (userId: string): Promise<UserProfile> => {
    const response = await api.get(`/users/${userId}`);
    return response.data;
  },

  // Обновить профиль
  updateProfile: async (data: UpdateProfileRequest): Promise<UserProfile> => {
    const response = await api.put('/user/profile', data);
    return response.data;
  },

  // Загрузить аватар
  uploadAvatar: async (file: File): Promise<{ Avatar: string }> => {
    const formData = new FormData();
    formData.append('avatar', file);

    const response = await api.post('/users/avatar', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  // Подтвердить email
  confirmEmail: async (code: string): Promise<void> => {
    await api.post('/users/confirm-email', { code });
  },

  // Отправить код подтверждения email
  resendConfirmationEmail: async (): Promise<void> => {
    await api.post('/users/resend-confirmation');
  },
};