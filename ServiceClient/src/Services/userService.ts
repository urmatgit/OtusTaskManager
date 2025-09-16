import { getCurrentUser } from "./authService";
const apiBaseUrl = 'http://localhost:5191/api/user'; // URL вашего Web API

export const getAllUsers = async () => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type':'application/json',
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const data = await response.json();
                return data;
            } else {
                console.log('Ошибка получения данных:', response.status);
            }
        } catch (error) {
            console.error('Ошибка:', error);
        }
    };