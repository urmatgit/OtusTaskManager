import { useEffect, useState } from "react";
import { keycloak } from "../Services/keycloak";

export const ProfilePage = () => {
  const [profile, setProfile] = useState<any>(null);

  useEffect(() => {
    const fetchProfile = async () => {
      const res = await fetch("https://localhost:7024/api/Account", {
        headers: { Authorization: `Bearer ${keycloak.token}` },
      });
      const data = await res.json();
      setProfile(data);
    };

    if (keycloak.token) fetchProfile();
  }, []);

  if (!profile) return <div>Загрузка...</div>;

  return (
    <div style={{ padding: 20 }}>
      <h2>Профиль</h2>
      <p>
        <strong>Логин:</strong> {profile.Username}
      </p>
      <p>
        <strong>Имя:</strong> {profile.FirstName}
      </p>
      <p>
        <strong>Фамилия:</strong> {profile.LastName}
      </p>
      <p>
        <strong>Email:</strong> {profile.Email}
      </p>
      <p>
        <strong>Телефон:</strong> {profile.Phone}
      </p>
    </div>
  );
};
