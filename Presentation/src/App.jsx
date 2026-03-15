import { useEffect, useState } from 'react'
import axios from 'axios'

function App() {
  const [data, setData] = useState([{ name: "Ładowanie danych..." }])
  const apiUrl = import.meta.env.VITE_API_URL || 'http://localhost:5000'

  useEffect(() => {
    axios.get(`${apiUrl}/data`)
      .then(res => {
        if (res.data && Array.isArray(res.data)) {
          setData(res.data)
        }
      })
      .catch(() => {
        setData([{ name: "Dane lokalne (Brak połączenia z API)" }])
      })
  }, [apiUrl])

  return (
    <div style={{ padding: '40px', color: 'white', backgroundColor: '#242424', minHeight: '100vh' }}>
      <h1>Projekt: cloud-app</h1>
      <p>Student: Filip Wójcik (95747)</p>
      <div style={{ border: '1px solid #646cff', padding: '20px', borderRadius: '12px' }}>
        <h2>Lista danych (Zadanie 3.2)</h2>
        <ul>
          {data.map((item, i) => (
            <li key={i} style={{ background: '#333', margin: '10px 0', padding: '10px' }}>
              {item.name}
            </li>
          ))}
        </ul>
      </div>
    </div>
  )
}

export default App