import { getCurrentUser } from "./authService";
const apiBaseUrl = 'http://localhost:5191/api/project'; // URL вашего Web API
const userPart = 'http://localhost:5191/api/user'; // URL вашего Web API
import { signalRService } from '../services/signalRService';

export const getAllProjects = async () => {
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

  export const createNewProject = async (name: string) => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}`, {
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

  export const updateProject = async (id: string, name: string, userid: string) => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}`, {
                method: 'PUT',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type':'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({
                    id,
                    name,
                    userid
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

  export const deleteNewProject = async (id: string) => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}/${id}`, {
                method: 'Delete',
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

  export const addUserToProject = async (id: string, userid: string) => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}/adduser`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type':'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({
                    id,
                    userid
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

  export const deleteUserFromProject = async (id: string, userid: string) => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${apiBaseUrl}/removeuser`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type':'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({
                    id,
                    userid
                })
            });

            if (response.ok) {
                const data = await response.json();
            
                alert("Участник удалён");
                return data;
            } else {
                alert('Ошибка получения данных.');
            }
        } catch (error) {
            alert('Ошибка: ${error}');
        }
    };

export const getUsers = async () => {
        try {
            const token = getCurrentUser()?.token;
            const response = await fetch(`${userPart}`, {
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