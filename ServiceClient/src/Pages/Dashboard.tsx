import React, { useEffect, useState } from 'react';
import '../styles/dashboard.css';
import { Button } from "antd";
import { getAllProjects } from "../Services/projectsService";
import { createNewProject } from "../Services/projectsService";

interface Project {
    id: string;
    name: string;
    create: string;
    owner: string;
}
const Dashboard: React.FC = () => {
    const [projects, setProjects] = useState<Project[]>([]);
    const [newProjectName, setNewProjectName] = useState('');
    const [showModal, setShowModal] = useState(false)

    useEffect(() => {
        const fetchProjects = async () => {
            try {
                const projectsData = await getAllProjects();
                setProjects(projectsData);
            } catch (error) {
                console.error(error);
            }
        };

        fetchProjects();
    }, []);
    
    const handleCreateProject = async () => {
        try {
            await createNewProject(newProjectName);
            const updatedProjectsList = await getAllProjects();

            setProjects(updatedProjectsList);
            setShowModal(false);
            setNewProjectName('');
        } catch (error) {
            console.error(error);
        }
    };

  return (
    <>
      <h1>Projects Dashboard</h1>
      <button onClick={() => setShowModal(true)}>Создать новый проект</button>
      {showModal && (
          <div className="modal">
              <h2>Создание нового проекта</h2>
              <input
                  type="text"
                  value={newProjectName}
                  onChange={(e) => setNewProjectName(e.target.value)}
                  placeholder="Название проекта"
              />
              <button onClick={handleCreateProject}>Подтвердить</button>
              <button onClick={() => setShowModal(false)}>Отменить</button>
          </div>
      )}
      <Button onClick={getAllProjects}>Обновить список проектов</Button>
        <div>
            <div>
                {projects.map((project) => (
                    <div key={project.id}>
                        <h3>{project.name}</h3>
                        <p>Создан: {new Date(project.create).toLocaleString()}</p>
                        <p>Владелец: {project.owner}</p>
                    </div>
                ))}
            </div>
        </div>
    </>
  );
};

export default Dashboard;