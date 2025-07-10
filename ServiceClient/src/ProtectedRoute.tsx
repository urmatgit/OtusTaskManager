import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "./Components/AuthContext";
import { Result, Button } from "antd";

interface ProtectedRouteProps {
  children: React.ReactNode;
  requiredRoles?: string[];
}

const ProtectedRoute: React.FC<ProtectedRouteProps> = ({
  children,
  requiredRoles = [],
}) => {
  const location = useLocation();
  const { isAuthenticated, isLoading, isAdmin, isUser } = useAuth();

  if (isLoading) {
    return (
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
        }}
      ></div>
    );
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Check roles if required
  const hasRequiredRole =
    requiredRoles.length === 0 ||
    (requiredRoles.includes("Admin") && isAdmin) ||
    (requiredRoles.includes("User") && isUser);

  if (!hasRequiredRole) {
    return (
      <Result
        status="403"
        title="403"
        subTitle="Sorry, you are not authorized to access this page."
        extra={
          <Button type="primary" href="/">
            Back Home
          </Button>
        }
      />
    );
  }

  return <>{children}</>;
};

export default ProtectedRoute;
