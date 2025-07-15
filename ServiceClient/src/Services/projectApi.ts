import axios from "axios";
import {type Project} from "../types/project";
const API_URL = "https://localhost:7024/api/Project";

export const getProjects = async () => {
    const response = await axios.get<Project[]>(API_URL);
    return response.data;
};

export const addProject = async (project: Omit<Project, "id">) => {
    const response = await axios.post<Project>(API_URL, {name: project.name});
    return response.data;
};

export const updateProject = async (id: string, project: Partial<Project>) => {
    const response = await axios.put<Project>(API_URL, project);
    return response.data;
};

export const deleteProject = async (id: string) => {
    await axios.delete(`${API_URL}/${id}`);
};