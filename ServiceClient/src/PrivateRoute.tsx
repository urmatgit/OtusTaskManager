import React from "react";
import { Navigate } from "react-router-dom";
import { getCurrentUser } from "./Services/authService";
interface PrivateRouteProps {
  children: React.ReactNode;
}

const isAuthenticated = () => {
  // Логика проверки аутентификации
  return getCurrentUser() !== null;
  //return sessionStorage.getItem('accessToken') !== null; // Пример
};

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
  return isAuthenticated() ? <>{children}</> : <Navigate to="/login" />;
};

export default PrivateRoute;
