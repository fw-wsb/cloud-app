import { useState, useEffect } from 'react';
import axios from 'axios';

const Dashboard = () => {
    const [tasks, setTasks] = useState([]);
    const [error, setError] = useState('');
    const apiUrl = import.meta.env.VITE_API_URL || 'http://localhost:8081/api';

    useEffect(() => {
        axios.get(`${apiUrl}/tasks`)
            .then(res => setTasks(res.data))
            .catch(() => setError('Błąd API: Nie udało się pobrać danych.'));
    }, [apiUrl]);

    const handleAdd = (name) => {
        if (!name || name.trim() === '') {
            setError('Walidacja: Nazwa zadania nie może być pusta!');
            return;
        }
        setError('');
        // Tutaj byłaby logika POST
    };

    return (
        <div style={{ padding: '20px', color: 'white' }}>
            <h1>Dashboard - Zadanie 4.4</h1>
            {error && <p style={{ color: 'red', fontWeight: 'bold' }}>{error}</p>}
            <ul>
                {tasks.map(t => <li key={t.id}>{t.name} - {t.isCompleted ? 'OK' : '...'}</li>)}
            </ul>
        </div>
    );
};

export default Dashboard;