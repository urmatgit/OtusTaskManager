import { Routes, Route,useNavigate } from 'react-router-dom';
import { LoginPage } from './Pages/LoginPage';
import { ProfilePage } from './Pages/ProfilePage';
import { PrivateRoute } from './Pages/PrivateRoute';

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
      <Route path="*" element={<LoginPage />} />
    </Routes>
  );
}