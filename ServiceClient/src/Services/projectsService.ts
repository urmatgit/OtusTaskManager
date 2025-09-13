import { getCurrentUser } from "./authService";
const apiBaseUrl = 'http://localhost:5191/api'; // URL вашего Web API

export const getAllProjects = async () => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}/project`, {
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

  export const createNewProject = async (name: string) => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}/project`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type':'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({
                    name
                })
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