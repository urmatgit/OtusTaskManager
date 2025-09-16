import { createBrowserRouter } from "react-router-dom";
import { App } from "./App";
import LoginPage from "./Pages/loginPage";
import Register from "./Pages/Register";
import Home from "./Pages/Home";
import Dashboard from "./Pages/Dashboard";
import ProjectList from "./Pages/ProjectList";
import KanbanBoard from "./Pages/KanbanBoard";
// import { Profile } from "./pages/Profile";
import { ProtectedRoute } from "./ProtectedRoute";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        index: true,
        element: (
          <ProtectedRoute>
            <Home />
          </ProtectedRoute>
        ),
      },
      {
        path: "dashboard",
        element: (
          <ProtectedRoute>
            <Dashboard />
          </ProtectedRoute>
        ),
      },
      {
        path: "ProjectList",
        element: (
          <ProtectedRoute>
            <ProjectList />
          </ProtectedRoute>
        ),
      },
      {
        path: "Board",
        element: (
          <ProtectedRoute>
            <KanbanBoard />
          </ProtectedRoute>
        ),
      },
      //   {
      //     path: "profile",
      //     element: (
      //       <ProtectedRoute>
      //         <Profile />
      //       </ProtectedRoute>
      //     ),
      //   },
      {
        path: "login",
        element: <LoginPage />,
      },
      {
        path: "register",
        element: <Register />,
      },
    ],
  },
]);
