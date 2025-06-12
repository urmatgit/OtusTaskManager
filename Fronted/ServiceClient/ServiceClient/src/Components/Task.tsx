import React from 'react';
import '../styles/kanban.css';

interface TaskType {
    id: number;
    description: string;
}

interface TaskProps {
    task: TaskType;
    deleteTask: () => void;
}

const Task: React.FC<TaskProps> = ({ task, deleteTask }) => {
    return (
        <div className="task-title">
            <span>{task.description}</span>
            <button className="button-dangeres" onClick={deleteTask}>Удалить задачу</button>
        </div>
    );
};

export default Task;