import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Navbar from './components/Navbar/Navbar';
import StudentsList from './pages/StudentsList/StudentsList';
import About from './pages/About/About';
import Login from './pages/Login/Login';
import Register from './pages/Register/Register';

function App() {
    return (
        <BrowserRouter>
            <Navbar />

            <div className="container">
                <Routes>
                    <Route path="/" element={<Navigate to="/login" replace />} />

                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />

                    <Route path="/dashboard" element={<StudentsList />} />
                    <Route path="/about" element={<About />} />
                </Routes>
            </div>
        </BrowserRouter>
    );
}

export default App;