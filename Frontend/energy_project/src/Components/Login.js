import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

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

    //print the header
    console.log(Headers)

    axios.post('https://localhost:7167/api/Login/Login', loginData)
      .then((response) => {
        // Assuming the JWT token is returned in the response
        const jwtToken = response.data;//.token;

        // Save the token to localStorage
        localStorage.setItem('jwtToken', jwtToken);

        // Handle successful login (e.g., redirect to a new page)
        navigate('/adminUserCRUD');
      })
      .catch((error) => {
        // Handle login error (e.g., display an error message)
        console.error('Login failed:', error);
      });
  };

  const handleRegister = () => {
    const registerData = {
      name: registerName,
      email: registerEmail,
      password: registerPassword,
      role: 'user', // Set the role to 'user' by default
    };

    axios.post('https://localhost:7167/api/Login/Register', registerData)
      .then((response) => {
        // Assuming the JWT token is returned in the response
        const jwtToken = response.data;//.token;

        // Save the token to localStorage
        localStorage.setItem('jwtToken', jwtToken);

        // Handle successful registration (e.g., redirect to a new page)
        // print a success message for now
        console.log('Registration succeeded!');
      })
      .catch((error) => {
        // Handle registration error (e.g., display an error message)
        console.error('Registration failed:', error);
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
      <button onClick={handleLogin}>Login</button>

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
      <button onClick={handleRegister}>Register</button>
    </div>
  );
};

export default Login;
