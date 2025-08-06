
// src/api/client.ts
import axios from 'axios';

export const api = axios.create({
  baseURL: 'http://localhost:5191/api',
  headers: { 'Content-Type': 'application/json' },
});

// Вызовите это при необходимости
export const setAuthHeader = (token: string | null) => {
  if (token) {
    api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    delete api.defaults.headers.common['Authorization'];
  }
};