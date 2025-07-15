import React, { useState, useEffect } from "react";
import {type Project} from "../types/project";
import {  getProjects, addProject, updateProject, deleteProject } from "../Services/projectApi";


const ProjectsList: React.FC = () => {
    const [projects, setProjects] = useState<Project[]>([]);
    const [newProject, setNewProject] = useState<Omit<Project, "id">>({ 
        name: "", 
        create: new Date().toISOString(), 
        owner: "00000000-0000-0000-0000-000000000000" 
    });
    const [editingProject, setEditingProject] = useState<Project | null>(null);

    useEffect(() => {
        fetchProjects();
    }, []);

    const fetchProjects = async () => {
        const data = await getProjects();
        setProjects(data);
    };

    const handleAddProject = async () => {
        await addProject(newProject);
        fetchProjects();
        setNewProject({ 
            name: "", 
            create: new Date().toISOString(), 
            owner: "00000000-0000-0000-0000-000000000000" 
        });
    };

    const handleUpdateProject = async () => {
        if (!editingProject) return;
        await updateProject(editingProject.id, editingProject);
        setEditingProject(null);
        fetchProjects();
    };

    const handleDeleteProject = async (id: string) => {
        await deleteProject(id);
        fetchProjects();
    };

    return (
        <div>
            <h1>Projects</h1>
            
            {/* Add Project */}
            <div>
                <h2>Add Project</h2>
                <input
                    type="text"
                    value={newProject.name}
                    onChange={(e) => setNewProject({ ...newProject, name: e.target.value })}
                    placeholder="Project Name"
                />
                <button onClick={handleAddProject}>Add</button>
            </div>

            {/* Edit Project (if editing) */}
            {editingProject && (
                <div>
                    <h2>Edit Project</h2>
                    <input
                        type="text"
                        value={editingProject.name}
                        onChange={(e) => setEditingProject({ ...editingProject, name: e.target.value })}
                    />
                    <button onClick={handleUpdateProject}>Save</button>
                    <button onClick={() => setEditingProject(null)}>Cancel</button>
                </div>
            )}

            {/* Projects List */}
            <ul>
                {projects.map((project) => (
                    <li key={project.id}>
                        {project.name} (Created: {new Date(project.create).toLocaleDateString()})
                        <button onClick={() => setEditingProject(project)}>Edit</button>
                        <button onClick={() => handleDeleteProject(project.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ProjectsList;