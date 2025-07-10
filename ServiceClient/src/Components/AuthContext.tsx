import { createContext, useContext, type ReactNode } from "react";
import { useKeycloak } from "@react-keycloak/web";

interface AuthContextType {
  isAdmin: boolean;
  isUser: boolean;
  isLoading: boolean;
  isAuthenticated: boolean;
  username: string;
  profile: {
    name?: string;
    email?: string;
  };
  login: () => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const { keycloak, initialized } = useKeycloak();

  const isAdmin = initialized && keycloak.hasRealmRole("Admin");
  const isUser = initialized && keycloak.hasRealmRole("User");
  const isAuthenticated = initialized && keycloak.authenticated;

  const profile = {
    name:
      keycloak.tokenParsed?.name || keycloak.tokenParsed?.preferred_username,
    email: keycloak.tokenParsed?.email,
  };

  const login = () => keycloak.login();
  const logout = () => keycloak.logout();

  const value = {
    isAdmin,
    isUser,
    isLoading: !initialized,
    isAuthenticated,
    username: keycloak.tokenParsed?.preferred_username || "",
    profile,
    login,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};
