import { Navigate } from 'react-router-dom';
import { useContext } from 'react';
import { AuthContext } from '../Components/AuthContext';

export const PrivateRoute = ({ children }: { children: JSX.Element }) => {
  const { authenticated } = useContext(AuthContext);
  if (authenticated === null) return <div>Загрузка...</div>;
  return authenticated ? children : <Navigate to="/login" />;
};