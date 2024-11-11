import React, { useState } from 'react';
import '../styles/AuthForm.css';

const AuthForm = ({ onLogin, onRegister }) => {
    const [isRegister, setIsRegister] = useState(false);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
  
    const handleSubmit = (e) => {
      e.preventDefault();
      if (isRegister) {
        if (password === confirmPassword) {
          onRegister(username, password); // Вызываем onRegister при регистрации
        } else {
          alert("Passwords do not match");
        }
      } else {
        onLogin(username, password); // Вызываем onLogin при входе
      }
    };
  
    const toggleForm = () => {
      setIsRegister(!isRegister);
      setUsername('');
      setPassword('');
      setConfirmPassword('');
    };
  
    return (
      <form className="auth-form" onSubmit={handleSubmit}>
        <h2>{isRegister ? 'Register' : 'Login'}</h2>
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
        {isRegister && (
          <input
            type="password"
            placeholder="Confirm Password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            required
          />
        )}
        <button type="submit">{isRegister ? 'Register' : 'Login'}</button>
        <p className="toggle-text" onClick={toggleForm}>
          {isRegister ? 'Already have an account? Log in' : "Don't have an account? Register"}
        </p>
      </form>
    );
  };
  
  export default AuthForm;
