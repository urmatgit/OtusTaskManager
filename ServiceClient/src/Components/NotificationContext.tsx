// contexts/NotificationContext.tsx
import { createContext, useContext } from "react";
import { notification } from "antd";
import type { NotificationInstance } from "antd/es/notification/interface";
type NotificationContextType = {
  notification: NotificationInstance;
};

const NotificationContext = createContext<NotificationContextType | undefined>(
  undefined
);

export const NotificationProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [api, contextHolder] = notification.useNotification();

  return (
    <NotificationContext.Provider value={{ notification: api }}>
      {contextHolder}
      {children}
    </NotificationContext.Provider>
  );
};

export const useNotification = () => {
  const context = useContext(NotificationContext);
  if (!context) {
    throw new Error("useNotification must be used within NotificationProvider");
  }
  return context.notification;
};
