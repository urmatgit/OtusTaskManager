import { notification } from "antd";
import { ReactNode } from "react";
import {
  CheckCircleFilled,
  CloseCircleFilled,
  ExclamationCircleFilled,
  InfoCircleFilled,
} from "@ant-design/icons";

type NotificationType = "success" | "info" | "warning" | "error";

interface NotificationProps {
  type: NotificationType;
  message: string;
  description?: string | ReactNode;
  duration?: number;
  placement?:
    | "top"
    | "topLeft"
    | "topRight"
    | "bottom"
    | "bottomLeft"
    | "bottomRight";
}

const iconMap = {
  success: <CheckCircleFilled style={{ color: "#52c41a" }} />,
  info: <InfoCircleFilled style={{ color: "#1890ff" }} />,
  warning: <ExclamationCircleFilled style={{ color: "#faad14" }} />,
  error: <CloseCircleFilled style={{ color: "#ff4d4f" }} />,
};

export const Notification = {
  open: ({
    type,
    message,
    description,
    duration = 4.5,
    placement = "topRight",
  }: NotificationProps) => {
    notification[type]({
      message,
      description,
      duration,
      placement,
      icon: iconMap[type],
      style: {
        borderRadius: "8px",
        border: `1px solid ${getBorderColor(type)}`,
      },
    });
  },

  success: (message: string, description?: string | ReactNode) => {
    Notification.open({ type: "success", message, description });
  },

  info: (message: string, description?: string | ReactNode) => {
    Notification.open({ type: "info", message, description });
  },

  warning: (message: string, description?: string | ReactNode) => {
    Notification.open({ type: "warning", message, description });
  },

  error: (message: string, description?: string | ReactNode) => {
    Notification.open({ type: "error", message, description });
  },
};

function getBorderColor(type: NotificationType): string {
  const colors = {
    success: "#b7eb8f",
    info: "#91d5ff",
    warning: "#ffe58f",
    error: "#ffccc7",
  };
  return colors[type];
}
