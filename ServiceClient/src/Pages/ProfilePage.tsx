import { Button, Typography } from 'antd';
import { useContext,useState,useEffect } from 'react';
import { AuthContext } from '../Components/AuthContext';
import {api} from '../Services/client';

const { Title } = Typography;

export const ProfilePage = () => {
  const { logout } = useContext(AuthContext);
  const [profile, setProfile] = useState<any>(null);

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const res = await api.get('/Profile');
        setProfile(res.data);
      } catch (err) {
        console.error(err);
      }
    };
    fetchProfile();
  }, []);

  return (
    <div style={{ padding: 20 }}>
      <Title level={2}>Профиль</Title>
      {profile && (
        <div>
          <p><strong>Логин:</strong> {profile.Username}</p>
          <p><strong>Email:</strong> {profile.Email}</p>
          <p><strong>Роли:</strong> {profile.Roles.join(', ')}</p>
        </div>
      )}
      <Button type="default" onClick={logout} style={{ marginTop: 20 }}>
        Выйти
      </Button>
    </div>
  );
};