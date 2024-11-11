import React from 'react';
import AuthForm from '../components/AuthForm';
import { useNavigate } from 'react-router-dom';
import { login } from '../services/auth';
import '../styles/AuthForm.css';

const AuthPage = () => {
    const navigate = useNavigate();
  
    // const handleLogin = async (username, password) => {
    //   try {
    //     await login(username, password);
    //     navigate('/home'); // Перенаправление на главную страницу после успешного входа
    //   } catch (error) {
    //     alert('Login failed. Please check your credentials.');
    //   }
    // };
    // Функция для регистрации через API
// const register = async (username, password) => {
//     const response = await fetch('/api/register', {
//       method: 'POST',
//       headers: {
//         'Content-Type': 'application/json',
//       },
//       body: JSON.stringify({ username, password }),
//     });
//     return response.json();
//   };
  
const handleLogin = (username, password) => {
    console.log("Logging in:", username, password);
    navigate('/home'); // Перенаправляем на главную страницу после входа
  };

  // Обработчик для регистрации
  const handleRegister = (username, password) => {
    console.log("Registering:", username, password);
    alert("Registration successful! You can now log in.");
    navigate('/'); // Перенаправляем на страницу входа после регистрации
  };

  return (
    <div className="auth-page">
      <AuthForm onLogin={handleLogin} onRegister={handleRegister} />
    </div>
  );
};
  
  export default AuthPage;