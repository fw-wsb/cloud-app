import { useState, useEffect } from 'react';
import axios from 'axios';

function App() {
    const [tasks, setTasks] = useState([]);
    const [newTaskName, setNewTaskName] = useState('');
    const [error, setError] = useState('');
    const apiUrl = 'https://task-manager-api-filip-dvf2bkg7gtc2d3cf.germanywestcentral-01.azurewebsites.net/api/tasks'; 

    const fetchTasks = () => {
        axios.get(apiUrl)
            .then(res => {
                setTasks(res.data);
                setError('');
            })
            .catch(() => setError('Błąd połączenia z API!'));
    };

    useEffect(() => {
        fetchTasks();
    }, []);

    const handleAddTask = (e) => {
        e.preventDefault();
        if (!newTaskName.trim()) return;

        axios.post(apiUrl, { name: newTaskName, isCompleted: false })
            .then(() => {
                setNewTaskName('');
                fetchTasks();
            })
            .catch(() => setError('Nie udało się dodać zadania.'));
    };

    const handleDeleteTask = (id) => {
        axios.delete(`${apiUrl}/${id}`)
            .then(() => {
                fetchTasks();
            })
            .catch(() => setError('Nie udało się usunąć zadania.'));
    };

    return (
        <div style={{ padding: '40px', background: '#0f172a', color: 'white', minHeight: '100vh', textAlign: 'center' }}>
            <h1>Projekt: cloud-app (Zadanie 8.4)</h1>
            <p>Student: Filip Wójcik (95747)</p>
            
            <form onSubmit={handleAddTask} style={{ margin: '20px 0' }}>
                <input 
                    type="text" 
                    value={newTaskName} 
                    onChange={(e) => setNewTaskName(e.target.value)}
                    placeholder="Wpisz nazwę zadania..."
                    style={{ padding: '10px', width: '250px' }}
                />
                <button type="submit" style={{ padding: '10px 20px', marginLeft: '10px', cursor: 'pointer' }}>
                    Dodaj zadanie
                </button>
            </form>

            {error && <p style={{ color: '#ff6b6b' }}>{error}</p>}

            <div style={{ background: '#1e293b', padding: '20px', borderRadius: '10px', display: 'inline-block', minWidth: '350px' }}>
                <ul style={{ listStyle: 'none', padding: 0 }}>
                    {tasks.map(t => (
                        <li key={t.id} style={{ borderBottom: '1px solid #334155', padding: '10px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                            <span>{t.name} {t.isCompleted ? '✅' : '⏳'}</span>
                            <button 
                                onClick={() => handleDeleteTask(t.id)} 
                                style={{ marginLeft: '20px', background: '#ef4444', color: 'white', border: 'none', padding: '5px 10px', borderRadius: '4px', cursor: 'pointer' }}
                            >
                                Usuń
                            </button>
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
}

export default App;