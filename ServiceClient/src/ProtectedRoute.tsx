import { getCurrentUser } from "./Services/authService";
import { Navigate, useLocation } from "react-router-dom";

interface PrivateRouteProps {
  children: React.ReactNode;
}
export const isAuthenticated = () => {
  // Логика проверки аутентификации
  return getCurrentUser() !== null;
  //return sessionStorage.getItem('accessToken') !== null; // Пример
};

export const ProtectedRoute: React.FC<PrivateRouteProps> = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const location = useLocation();
  const user = getCurrentUser();

  if (!user) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <>{children}</>;
};
