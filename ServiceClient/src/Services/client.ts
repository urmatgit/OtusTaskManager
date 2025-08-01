// src/api/client.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7024/api/Account/', // ваш ASP.NET Core API
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;