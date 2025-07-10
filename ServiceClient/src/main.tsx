import { StrictMode } from "react";
import ReactDOM from "react-dom/client";
import { createRoot } from "react-dom/client";
//import { RouterProvider } from "react-router-dom";
// import { router } from "./routes.tsx1";
// import { NotificationProvider } from "./Components/NotificationContext";
import "./index.css";
import App from "./App.tsx";
import axios from "axios";
import { ReactKeycloakProvider } from "@react-keycloak/web";
import { keycloak } from "./Services/keycloak";
const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
// Add token to axios requests
const onKeycloakTokens = (tokens: { token?: string }) => {
  if (tokens.token) {
    axios.defaults.headers.common["Authorization"] = `Bearer ${tokens.token}`;
    localStorage.setItem("token", tokens.token);
  } else {
    delete axios.defaults.headers.common["Authorization"];
    localStorage.removeItem("token");
  }
};

root.render(
  <ReactKeycloakProvider
    authClient={keycloak}
    onTokens={onKeycloakTokens}
    initOptions={{ onLoad: "login-required" }}
  >
    <StrictMode>
      <App />
      {/* <NotificationProvider>
      <RouterProvider router={router} />
    </NotificationProvider> */}
    </StrictMode>
  </ReactKeycloakProvider>
);
