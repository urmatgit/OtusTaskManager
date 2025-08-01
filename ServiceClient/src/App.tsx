import { Routes, Route } from "react-router-dom";
import { LoginPage } from "./Pages/LoginPage1";
import { RegisterPage } from "./Pages/RegisterPage";
import { ProfilePage } from "./Pages/ProfilePage";

function App() {
  return (
    <div
      style={{
        minHeight: "100vh",
        padding: "20px",
        backgroundColor: "#f0f2f5",
      }}
    >
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="*" element={<RegisterPage />} />
      </Routes>
    </div>
  );
}

export default App;
