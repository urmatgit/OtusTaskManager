import axios from 'axios';
import  {UserRole} from '../Models/user';

//const API_URL = 'https://localhost:7024/api/auth/';
const API_URL = 'http://localhost:5191/api/auth/';


export type RegisterData = {
  FirstName: string;
  LastName: string;
  Username: string;
  Email: string;
  Phone: string;
  Password: string;
  Role: UserRole;
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
  Role: UserRole;
  token: string;
  tokenExpiration: string;
};

// Регистрация пользователя
export const register = async (data: RegisterData): Promise<UserData> => {
  try {
    const response = await axios.post(`${API_URL}register`, data);
    
    if (response.data.token) {
      // Успешная регистрация
      
      const userData: UserData = {
        ...response.data.user,
        token: response.data.token,
        tokenExpiration: response.data.expiration,
        Role:response.data.role
      };
      
      localStorage.setItem('user', JSON.stringify(userData));
      return userData;
    }
    return response.data;
  } catch (error) {
    let errorMessage = 'Ошибка регистрации. Пожалуйста, попробуйте ещё раз.';
    console.error(error);
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
    
    
    
    throw new Error(errorMessage);
  }
};

// Авторизация пользователя
export const login = async (data: LoginData): Promise<UserData> => {
  try {
    const response = await axios.post(`${API_URL}login`, data);

    if (response.data.token) {
      // Успешный вход
       

      const userData: UserData = {
        ...response.data.user,
        token: response.data.token,
        tokenExpiration: response.data.expiration,
        
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
    
    
    
    throw new Error(errorMessage);
  }
};
//Проверка токена истекший
 const isTokenExpired = (token: string | null): boolean => {
  if (!token) return true;
  
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const exp = payload.exp;
    const now = Date.now() / 1000;
    
    return exp < now;
  } catch (error) {
    console.error('Error decoding token:', error);
    return true;
  }
};
const isDateExpired = (expiration: string | null): boolean => {
  if (!expiration) return true;
  
  try {
    //server expiration UTC date
    const utcDate=new Date(expiration);
    //current local time as milliseconds
    const now = Date.now();
    //convert local time to UTC date
    
    
    return utcDate.getTime() < now;
  } catch (error) {
    console.error('Error decoding token:', error);
    return true;
  }
};
// Выход из системы
export const logout = (): void => {
  localStorage.removeItem('user');
   
};

// Получение текущего пользователя
export const getCurrentUser = (): UserData | null => {
  const userStr = localStorage.getItem('user');
  if (userStr===null) return null;
  const userData=JSON.parse(userStr) ;
  if (isDateExpired(userData.tokenExpiration)) {
    localStorage.removeItem('user');
    return null;
  }
  return userData;
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
export const hasRole = (role: UserRole): boolean => {
  const user = getCurrentUser();
  return user?.role === role || user?.role == UserRole[role];
};