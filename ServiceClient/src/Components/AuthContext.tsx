import React, { createContext, useState, useEffect } from 'react';
import {keycloak} from "../Services/keycloak";

export const AuthContext = createContext({
  authenticated: null as boolean | null,
  token: null as string | null,
  login: () => {},
  logout: () => {},
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [authenticated, setAuthenticated] = useState<boolean | null>(null);
  const [token, setToken] = useState<string | null>(null);
 const [kcInitialized, setKcInitialized] = useState(false);
  useEffect(() => {
    const init = async () => {
      if (!kcInitialized){
      const auth = await keycloak.init({
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri: window.location.origin + '/silent-check-sso.html',
       });
       setAuthenticated(auth);
      if (auth) setToken(keycloak.token);
       setKcInitialized(true);
      }
      

      // Обновление токена
      const interval = setInterval(() => {
        keycloak.updateToken(60).then(refreshed => {
          if (refreshed) setToken(keycloak.token);
        });
      }, 300000); // 5 минут

      return () => clearInterval(interval);
    };

    init();
  }, []);

  const login = () => keycloak.login();
  const logout = () => keycloak.logout({ redirectUri: window.location.origin });

  return (
    <AuthContext.Provider value={{ authenticated, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};