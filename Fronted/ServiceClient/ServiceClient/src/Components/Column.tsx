import React, { useState } from 'react';
import Task from './Task';
import '../styles/authorize.css';

interface TaskType {
    id: number;
    description: string;
}

interface ColumnType {
    id: number;
    title: string;
    tasks: TaskType[];
}

interface ColumnProps {
    column: ColumnType;
    updateColumnTitle: (id: number, title: string) => void;
    deleteColumn: (id: number) => void;
    addTask: (columnId: number, description: string) => void;
    deleteTask: (columnId: number, taskId: number) => void;
}

const Column: React.FC<ColumnProps> = ({ column, updateColumnTitle, deleteColumn, addTask, deleteTask }) => {
    const [newTaskDescription, setNewTaskDescription] = useState('');
    const [columnTitle, setColumnTitle] = useState(column.title);

    const handleAddTask = () => {
        addTask(column.id, newTaskDescription);
        setNewTaskDescription('');
    };

    return (
        <div className="column" style={{ margin: '10px', padding: '10px', border: '1px solid black', width: '200px' }}>
            <input
                className="column-title"
                type="text"
                value={columnTitle}
                onChange={(e) => setColumnTitle(e.target.value)}
                onBlur={() => updateColumnTitle(column.id, columnTitle)}
            />
            <button className="button-dangeres" onClick={() => deleteColumn(column.id)}>Удалить колонку</button>
            <div>
                <input
                    className="task-title"
                    type="text"
                    value={newTaskDescription}
                    onChange={(e) => setNewTaskDescription(e.target.value)}
                    placeholder="Название задачи"
                />
                <button className="button-success" onClick={handleAddTask}>Добавить задачу</button>
            </div>
            {column.tasks.map(task => (
                <Task key={task.id} task={task} deleteTask={() => deleteTask(column.id, task.id)} />
            ))}
        </div>
    );
};

export default Column;