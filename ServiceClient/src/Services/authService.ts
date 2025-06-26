import axios from 'axios';
import { notification } from 'antd';

const API_URL = 'https://localhost:7024/api/auth/';

// Типы данных
export enum ProjectRole {
  User = 1,
  Admin, 
  Owner ,
  Editor
}

export type RegisterData = {
  FirstName: string;
  LastName: string;
  Username: string;
  Email: string;
  Phone: string;
  Password: string;
  Role: ProjectRole;
};

export type LoginData = {
  username: string;
  password: string;
};

export type UserData = {
  id: string;
  FirstName: string;
  LastName: string;
  Username: string;
  Email: string;
  Phone: string;
  Role: ProjectRole;
  token: string;
  tokenExpiration: string;
};

// Регистрация пользователя
export const register = async (data: RegisterData): Promise<UserData> => {
  try {
    const response = await axios.post(`${API_URL}register`, data);
    
    if (response.data.token) {
      // Успешная регистрация
      notification.success({
        message: 'Регистрация успешна',
        description: 'Аккаунт успешно создан!',
        placement: 'topRight',
        duration: 4.5
      });

      const userData: UserData = {
        ...response.data.user,
        token: response.data.token,
        tokenExpiration: response.data.expiration
      };
      
      localStorage.setItem('user', JSON.stringify(userData));
      return userData;
    }
    return response.data;
  } catch (error) {
    let errorMessage = 'Ошибка регистрации. Пожалуйста, попробуйте ещё раз.';
    
    if (axios.isAxiosError(error)) {
      if (error.response) {
        // Обработка ошибок сервера
        errorMessage = error.response.data.detail || errorMessage;
        
        // Обработка ошибок валидации
        if (error.response.data.errors) {
          const validationErrors = Object.values(error.response.data.errors).flat();
          errorMessage = validationErrors.join('\n');
        }
      } else if (error.request) {
        errorMessage = 'Ошибка сети - не удалось подключиться к серверу';
      }
    }
    
    notification.error({
      message: 'Ошибка регистрации',
      description: errorMessage,
      placement: 'topRight',
      duration: 5
    });
    
    throw new Error(errorMessage);
  }
};

// Авторизация пользователя
export const login = async (data: LoginData): Promise<UserData> => {
  try {
    const response = await axios.post(`${API_URL}login`, data);

    if (response.data.token) {
      // Успешный вход
      notification.success({
        message: 'Вход выполнен',
        description: 'Вы успешно авторизовались!',
        placement: 'topRight',
        duration: 4.5
      });

      const userData: UserData = {
        ...response.data.user,
        token: response.data.token,
        tokenExpiration: response.data.expiration
      };
      
      localStorage.setItem('user', JSON.stringify(userData));
      return userData;
    }
    return response.data;
  } catch (error) {
    let errorMessage = 'Ошибка входа. Проверьте правильность данных.';
    
    if (axios.isAxiosError(error)) {
      if (error.response) {
        errorMessage = error.response.data.message || errorMessage;
      } else if (error.request) {
        errorMessage = 'Ошибка сети - не удалось подключиться к серверу';
      }
    }
    
    notification.error({
      message: 'Ошибка авторизации',
      description: errorMessage,
      placement: 'topRight',
      duration: 5
    });
    
    throw new Error(errorMessage);
  }
};

// Выход из системы
export const logout = (): void => {
  localStorage.removeItem('user');
  notification.info({
    message: 'Выход выполнен',
    description: 'Вы успешно вышли из системы',
    placement: 'topRight'
  });
};

// Получение текущего пользователя
export const getCurrentUser = (): UserData | null => {
  const userStr = localStorage.getItem('user');

  return userStr ? JSON.parse(userStr) : null;
};

// Заголовок авторизации для запросов
export const authHeader = (): { Authorization: string } | null => {
  const user = getCurrentUser();
  if (user && user.token) {
    return { Authorization: `Bearer ${user.token}` };
  }
  return null;
};

// Проверка роли пользователя
export const hasRole = (role: ProjectRole): boolean => {
  const user = getCurrentUser();
  return user?.role === role;
};