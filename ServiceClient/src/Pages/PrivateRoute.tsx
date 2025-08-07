// src/components/PrivateRoute.tsx
import { Navigate, useLocation } from 'react-router-dom';
import { useContext } from 'react';
import { AuthContext } from '../Components/AuthContext';

export const PrivateRoute = ({ children }: { children: JSX.Element }) => {
  const { authenticated } = useContext(AuthContext);
  const location = useLocation();

  if (authenticated === null) {
    return <div>Загрузка...</div>;
  }

  if (!authenticated) {
    // 🔥 Сохраняем, куда пользователь хотел попасть
    sessionStorage.setItem('postLoginRedirect', location.pathname);
    return <Navigate to="/login" replace />;
  }

  return children;
};