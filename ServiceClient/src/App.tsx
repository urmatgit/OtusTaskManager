import { Routes, Route,useNavigate } from 'react-router-dom';
import { LoginPage } from './Pages/LoginPage';
import { ProfilePage } from './Pages/ProfilePage';
import { PrivateRoute } from './Pages/PrivateRoute';
import {HomePage} from './Pages/HomePage';
import {AdminPage} from './Pages/AdminPage';
export default function App() {
  const navigate = useNavigate();
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/profile" element={
        <PrivateRoute>
          <ProfilePage />
        </PrivateRoute>
      } />
      <Route path="/admin" element={
        <PrivateRoute>
          <AdminPage />
        </PrivateRoute>
      } />
      <Route path="*" element={<HomePage />} />
    </Routes>
  );
}