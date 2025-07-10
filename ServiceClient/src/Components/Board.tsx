import React, { useState } from "react";
import Column from "./Column";
import "../styles/kanban.css";

interface ColumnType {
  id: number;
  title: string;
  tasks: TaskType[];
}

interface TaskType {
  id: number;
  description: string;
}

const Board: React.FC = () => {
  const [columns, setColumns] = useState<ColumnType[]>([]);
  const [columnTitle, setColumnTitle] = useState("");

  const addColumn = () => {
    const newColumn: ColumnType = {
      id: Date.now(),
      title: columnTitle,
      tasks: [],
    };
    setColumns([...columns, newColumn]);
    setColumnTitle("");
  };

  const deleteColumn = (id: number) => {
    setColumns(columns.filter((column) => column.id !== id));
  };

  const updateColumnTitle = (id: number, title: string) => {
    setColumns(
      columns.map((column) =>
        column.id === id ? { ...column, title } : column
      )
    );
  };

  const addTask = (columnId: number, description: string) => {
    const newTask: TaskType = {
      id: Date.now(),
      description,
    };
    setColumns(
      columns.map((column) =>
        column.id === columnId
          ? { ...column, tasks: [...column.tasks, newTask] }
          : column
      )
    );
  };

  const deleteTask = (columnId: number, taskId: number) => {
    setColumns(
      columns.map((column) =>
        column.id === columnId
          ? {
              ...column,
              tasks: column.tasks.filter((task) => task.id !== taskId),
            }
          : column
      )
    );
  };

  return (
    <div className="marg">
      <h1>Kanban Board</h1>
      <input
        type="text"
        value={columnTitle}
        onChange={(e) => setColumnTitle(e.target.value)}
        placeholder="Название новой колонки"
      />
      <button className="button-success" onClick={addColumn}>
        Добавить колонку
      </button>
      <div style={{ display: "flex" }}>
        {columns.map((column) => (
          <Column
            key={column.id}
            column={column}
            updateColumnTitle={updateColumnTitle}
            deleteColumn={deleteColumn}
            addTask={addTask}
            deleteTask={deleteTask}
          />
        ))}
      </div>
    </div>
  );
};

export default Board;
