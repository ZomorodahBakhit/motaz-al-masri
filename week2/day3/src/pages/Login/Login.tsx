import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axiosInstance from '../../api/axiosInstance';
import './Auth.scss';

const Login: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errorMsg, setErrorMsg] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setErrorMsg('');

        try {
            const response = await axiosInstance.post('/Auth/login', {
                email,
                password
            });

            console.log("the backend response", response.data);
            const token = response.data.result?.message;
            if (token) {
                localStorage.setItem('token', token);
                console.log('logged in successfully, and token has been saved');

                navigate('/dashboard');
            } else {
                setErrorMsg('error could not save the token');
            }

        } catch (error: any) {
            console.error('log in error', error);
            setErrorMsg(error.response?.data?.message || 'wrong email or password');
        }
    };

    return (
        <div className="auth-container">
            <div className="auth-card">
                <h2>log in</h2>

                {errorMsg && <div style={{ color: '#dc3545', backgroundColor: '#f8d7da', padding: '10px', borderRadius: '5px', marginBottom: '15px', textAlign: 'center', fontWeight: 'bold' }}>{errorMsg}</div>}

                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>Email</label>
                        <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                    </div>
                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                    </div>
                    <button type="submit" className="btn-submit">دخول</button>
                </form>
                <div className="auth-links">
                    <p>don't have account <Link to="/register">Register as new Teacher</Link></p>
                </div>
            </div>
        </div>
    );
};

export default Login;