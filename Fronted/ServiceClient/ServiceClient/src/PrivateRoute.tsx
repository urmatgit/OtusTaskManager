import React from 'react';
import { Navigate } from 'react-router-dom';

interface PrivateRouteProps {
    children: React.ReactNode;
}

const isAuthenticated = () => {
    // Логика проверки аутентификации
    return sessionStorage.getItem('accessToken') !== null; // Пример
};

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
    return isAuthenticated() ? <>{children}</> : <Navigate to="/auth" />;
};

export default PrivateRoute;