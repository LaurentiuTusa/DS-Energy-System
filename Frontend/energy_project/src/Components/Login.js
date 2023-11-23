import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Login = () => {
  const [loginEmail, setLoginEmail] = useState('');
  const [loginPassword, setLoginPassword] = useState('');
  const [registerName, setRegisterName] = useState('');
  const [registerEmail, setRegisterEmail] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');

  const navigate = useNavigate();

  const handleLogin = () => {
    const loginData = {
      email: loginEmail,
      password: loginPassword,
    };

    axios.post('http://localhost:8082/api/Login/Login', loginData)
      .then((response) => {
        const jwtToken = response.data.token;
        const currentUserId = response.data.currentUserId;
        const currentUserRole = response.data.currentUserRole;

        // Save the token, currentUserId and currentUserRole to localStorage
        localStorage.setItem('jwtToken', jwtToken);
        localStorage.setItem('currentUserId', currentUserId);
        localStorage.setItem('currentUserRole', currentUserRole);

        console.log('From Login:');
        console.log('jwtToken:', jwtToken);
        console.log('currentUserRole:', currentUserRole);
        console.log('currentUserId:', currentUserId);

        // Redirect to the appropriate dashboard based on the user's role
        if (currentUserRole === 'admin') {
          navigate(`/adminUserCRUD/${currentUserId}`);
        } else if (currentUserRole === 'user') {
          navigate(`/userDashboard/${currentUserId}`);
        }
      })
      .catch((error) => {
        console.error('Login failed:', error);
      });
  };

  const handleRegister = () => {

    const registerUrl = 'http://localhost:8082/api/Login/Register';

    const registerData = {
      name: registerName,
      email: registerEmail,
      password: registerPassword,
      role: 'user', // Set the role to 'user' by default
    };

    axios.post(registerUrl, registerData, {
      headers: {
        AccessControlAllowOrigin: '*',
      }
    })
      .then((registerResult) => {

        const jwtToken = registerResult.data.token;
        const currentUserId = registerResult.data.currentUserId;
        const currentUserRole = registerResult.data.currentUserRole;

        // Save the token, currentUserId and currentUserRole to localStorage
        localStorage.setItem('jwtToken', jwtToken);
        localStorage.setItem('currentUserId', currentUserId);
        localStorage.setItem('currentUserRole', currentUserRole);

        navigate(`/userDashboard/${currentUserId}`);
      })
      .catch((error) => {
        console.error('Registration failed at the login part:', error);
      });
  };

  return (
    <div>
      <h2>Login</h2>
      <label>Email:
        <input type="text" value={loginEmail} onChange={(e) => setLoginEmail(e.target.value)} />
      </label>
      <label>Password:
        <input type="password" value={loginPassword} onChange={(e) => setLoginPassword(e.target.value)} />
      </label>
      <button className="btn btn-primary" onClick={handleLogin}>Login</button>

      <h2>Register</h2>
      <label>Name:
        <input type="text" value={registerName} onChange={(e) => setRegisterName(e.target.value)} />
      </label>
      <label>Email:
        <input type="text" value={registerEmail} onChange={(e) => setRegisterEmail(e.target.value)} />
      </label>
      <label>Password:
        <input type="password" value={registerPassword} onChange={(e) => setRegisterPassword(e.target.value)} />
      </label>
      <button className="btn btn-primary" onClick={handleRegister}>Register</button>
    </div>
  );
};

export default Login;
