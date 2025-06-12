import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './Pages/Home.tsx';
import AuthPage from './Pages/AuthPage.tsx';
import Dashboard from './Pages/Dashboard.tsx';
import PrivateRoute from './PrivateRoute.tsx';
import KanbanBoard from './Components/Board.tsx';
import './styles/authorize.css'

const App: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/auth" element={<AuthPage />} />
                <Route path="/dashboard" element={<PrivateRoute><Dashboard /></PrivateRoute>} />
                <Route path="/Board" element={<PrivateRoute><KanbanBoard /></PrivateRoute>} />
            </Routes>
        </Router>
    );
};

export default App;