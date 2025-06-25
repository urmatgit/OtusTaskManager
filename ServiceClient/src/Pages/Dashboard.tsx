import React, { useState } from 'react';
import '../styles/kanban.css';
import { PlusOutlined, DeleteOutlined } from '@ant-design/icons';

const Dashboard: React.FC = () => {
    const [tasks, setTasks] = useState<any[]>([]);
    const [columns, setColumns] = useState<string[]>(['To Do', 'In Progress', 'Done']);

    const addTask = (title: string) => {
        setTasks(prevTasks => [...prevTasks, { title, status: 'To Do' }]);
    };

    const moveTask = (taskId: string, status: string) => {
        setTasks(prevTasks =>
            prevTasks.map(task =>
                task.id === taskId ? { ...task, status } : task
            )
        );
    };

    const addColumn = () => {
        setColumns(prevColumns => [...prevColumns, `Новый этап ${prevColumns.length + 1}`]);
    };

    const removeColumn = (columnIndex: number) => {
        setColumns(prevColumns => prevColumns.filter((_, idx) => idx !== columnIndex));
    };

    return (
        <div columns={columns} data={tasks}>
            {({ column, data }) => (
                <div>
                    <div className="ant-pro-layout-kanban-column-title">{column.title}</div>
                    {data.map((task, idx) => (
                        <div key={idx} className="ant-pro-layout-kanban-item">
                            <div className="ant-pro-layout-kanban-item-title">{task.title}</div>
                            <div className="ant-pro-layout-kanban-item-status">{task.status}</div>
                            <div className="ant-pro-layout-kanban-item-actions">
                                <button onClick={() => moveTask(task.id, 'In Progress')}>
                                    Переместить в прогресс
                                </button>
                                <button onClick={() => moveTask(task.id, 'Done')}>
                                    Завершить
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            )}
            <div className="ant-pro-layout-kanban-add">
                <button onClick={addTask}>
                    <PlusOutlined /> Добавить задачу
                </button>
            </div>
            <div className="ant-pro-layout-kanban-column-actions">
                <button onClick={addColumn}>
                    <PlusOutlined /> Добавить столбец
                </button>
                {columns.map((column, idx) => (
                    <button key={idx} onClick={removeColumn}>
                        <DeleteOutlined /> Удалить столбец
                    </button>
                ))}
            </div>
        </div>
    );
};

export default Dashboard;