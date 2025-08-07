import { Button, Form } from 'antd';
import { useContext } from 'react';
import { AuthContext } from '../Components/AuthContext';

export const LoginPage = () => {
  const { login } = useContext(AuthContext);
const handleLogin = () => {
    // Пример: после входа — на /profile
    login('/profile');
  };

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <h2>Вход</h2>
        <Form onFinish={handleLogin}>
          <Button type="primary" htmlType="submit" size="large" block>
            Войти через Keycloak
          </Button>
        </Form>
        
      </div>
    </div>
  );
};

const styles = {
  container: { display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '80vh' },
  card: { padding: '40px', borderRadius: '8px', boxShadow: '0 4px 12px rgba(0,0,0,0.1)', backgroundColor: 'white', width: '100%', maxWidth: 500 },
  link: { textAlign: 'center', marginTop: '16px' }
};