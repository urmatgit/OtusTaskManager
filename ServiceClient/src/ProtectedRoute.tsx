import { notification } from "antd";
import { getCurrentUser } from "./Services/authService";
import { Navigate, useLocation } from "react-router-dom";

interface PrivateRouteProps {
  children: React.ReactNode;
}

export const ProtectedRoute: React.FC<PrivateRouteProps> = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const location = useLocation();
  const user = getCurrentUser();
  const [notificationApi, contextHolder] = notification.useNotification();
  if (!user) {
    notificationApi.warning(
      "Требуется авторизация",
      "Пожалуйста, войдите в систему"
    );
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return (
    <>
      {contextHolder}
      {children}
    </>
  );
};
