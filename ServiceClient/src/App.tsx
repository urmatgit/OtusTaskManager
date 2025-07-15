import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { ConfigProvider, Layout, theme } from "antd";
import HeaderWithMenu from "./Components/HeaderWithMenu";
import { AuthProvider } from "./Components/AuthContext";

import "./App.css";

// Pages
import HomePage from "./Pages/Home";
import DashboardPage from "./Pages/Dashboard";
import KanbanBoardPage from "./Pages/KanbanBoard";
import LoginPage1 from "./Pages/LoginPage1";
import NotFoundPage from "./Pages/NotFoundPage";
import ProtectedRoute from "./ProtectedRoute";
import ProjectsPage from "./Pages/ProjectsPage";
const { Content, Footer } = Layout;

const App: React.FC = () => {
  return (
    <ConfigProvider
      theme={{
        algorithm: theme.defaultAlgorithm,
        token: {
          colorPrimary: "#1890ff",
          borderRadius: 4,
          colorBgContainer: "#ffffff",
        },
      }}
    >
      <AuthProvider>
        <Router>
          <Layout style={{ minHeight: "100vh" }}>
            <HeaderWithMenu />
            <Content style={{ padding: "24px 48px" }}>
              <div
                style={{
                  minHeight: "calc(100vh - 188px)",
                  background: "#fff",
                  padding: 24,
                  borderRadius: 4,
                }}
              >
                <Routes>
                  <Route path="/login" element={<LoginPage1 />} />
                  <Route
                    path="/"
                    element={
                      <ProtectedRoute>
                        <HomePage />
                      </ProtectedRoute>
                    }
                  />
                  <Route
                    path="/dashboard"
                    element={
                      <ProtectedRoute >
                        <DashboardPage />
                      </ProtectedRoute>
                    }
                  />
                  <Route
                    path="/kanban"
                    element={
                      <ProtectedRoute requiredRoles={["Admin"]}>
                        <KanbanBoardPage />
                      </ProtectedRoute>
                    }
                  />
                  <Route
                    path="/projects"
                    element={
                      <ProtectedRoute >
                        <ProjectsPage />
                      </ProtectedRoute>
                    }
                  />
                  <Route path="*" element={<NotFoundPage />} />
                </Routes>
              </div>
            </Content>
            <Footer style={{ textAlign: "center" }}>
              MyApp ©{new Date().getFullYear()} Created by Your Team
            </Footer>
          </Layout>
        </Router>
      </AuthProvider>
    </ConfigProvider>
  );
};

export default App;
