import React, { useState } from 'react';
import '../styles/kanban.css';

interface Task {
    title: string;
    status: string;
    id: string;
}

interface Column {
    title: string;
    tasks: Task[];
}

const KanbanBoard: React.FC = () => {
    const [columns, setColumns] = useState<Column[]>([
        { title: 'To Do', tasks: [] },
        { title: 'In Progress', tasks: [] },
        { title: 'Done', tasks: [] },
    ]);

    const addColumn = () => {
        setColumns(prev => [...prev, { title: 'Новый этап', tasks: [] }]);
    };

    const removeColumn = (index: number) => {
        setColumns(prev => prev.filter((_, idx) => idx !== index));
    };

    const updateColumnTitle = (index: number, title: string) => {
        setColumns(prev =>
            prev.map((col, idx) => idx === index ? { ...col, title } : col)
        );
    };

    const addTask = (columnIndex: number, title: string) => {
        const newTasks = [...columns[columnIndex].tasks, { title, status: 'To Do', id: Date.now().toString() }];
        setColumns(prev =>
            prev.map((col, idx) => idx === columnIndex ? { ...col, tasks: newTasks } : col)
        );
    };

    const moveTask = (taskId: string, status: string, columnIndex: number) => {
        const updatedTasks = columns[columnIndex].tasks.map(task =>
            task.id === taskId ? { ...task, status } : task
        );
        setColumns(prev =>
            prev.map((col, idx) => idx === columnIndex ? { ...col, tasks: updatedTasks } : col)
        );
    };

    const removeTask = (taskId: string, columnIndex: number) => {
        const updatedTasks = columns[columnIndex].tasks.filter(task => task.id !== taskId);
        setColumns(prev =>
            prev.map((col, idx) => idx === columnIndex ? { ...col, tasks: updatedTasks } : col)
        );
    };

    return (
        <div className= "kanban-board" >
        {
            columns.map((column, idx) => (
                <div key= { idx } className = "column" >
                <div className="column-title" > { column.title } </div>
          {
                    column.tasks.map((task, taskIdx) => (
                        <div key= { taskIdx } className = "task" >
                        <div className="task-title" > { task.title } </div>
                    < div className = "task-status" > { task.status } </div>
                    < div className = "task-actions" >
                    <button onClick={() => moveTask(task.id, 'In Progress', idx)}>
                        Переместить в прогресс
                            </button>
                            < button onClick = {() => moveTask(task.id, 'Done', idx)}>
                                Завершить
                                </button>
                                < button onClick = { removeTask.bind(null, task.id, idx) } >
                                    Удалить
                                    </button>
                                    </div>
                                    </div>
          ))}
<button onClick={ addTask.bind(null, idx) }>
    Добавить задачу
        </button>
        </div>
      ))}
<button onClick={ addColumn }> Добавить столбец </button>
{
    columns.map((col, idx) => (
        <button key= { idx } onClick = { removeColumn.bind(null, idx) } >
        Удалить столбец
    </button>
    ))
}
</div>
  );
};

export default KanbanBoard;
