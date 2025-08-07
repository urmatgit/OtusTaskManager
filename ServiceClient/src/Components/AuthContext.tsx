// src/context/AuthContext.tsx
import React, { createContext, useState, useEffect } from 'react';
import {keycloak} from '../Services/keycloak';
import { setAuthHeader } from '../Services/client';

interface AuthContextType {
  authenticated: boolean | null;
  token: string | null;
  login: (redirectUri?: string) => void;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextType>({
  authenticated: null,
  token: null,
  login: () => {},
  logout: () => {},
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [authenticated, setAuthenticated] = useState<boolean | null>(null);
  const [token, setToken] = useState<string | null>(null);

  // Функция входа с указанием, куда редиректить
  const login = (redirectUri: string = '/profile') => {
    // Сохраняем целевой URL
    sessionStorage.setItem('postLoginRedirect', redirectUri);
    // Переходим на Keycloak
    keycloak.login({
      redirectUri: window.location.origin,
    });
  };

  const logout = () => {
    sessionStorage.removeItem('postLoginRedirect');
    keycloak.logout({
      redirectUri: window.location.origin,
    });
  };

  useEffect(() => {
    const initKeycloak = async () => {
      try {
        const auth = await keycloak.init({
          onLoad: 'check-sso',
          silentCheckSsoRedirectUri: window.location.origin + '/silent-check-sso.html',
        });

        setAuthenticated(auth);
        if (auth) {
          const newToken = keycloak.token;
          setToken(newToken);
          setAuthHeader(newToken); // ✅ Устанавливаем заголовок при входе
// 🔁 Получаем URL, на который нужно перейти
          const redirectUrl = sessionStorage.getItem('postLoginRedirect') ;
          if (redirectUrl!==null){
            sessionStorage.removeItem('postLoginRedirect');

            // 🔁 Перенаправляем
            window.location.href = redirectUrl;
          }
        } else {
          setAuthHeader(null); // Убираем, если не авторизован
        }
      } catch (error) {
        console.error('Keycloak init failed', error);
        setAuthenticated(false);
        setAuthHeader(null);
      }
    };

    initKeycloak();
  }, []);

  return (
    <AuthContext.Provider value={{ authenticated, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};