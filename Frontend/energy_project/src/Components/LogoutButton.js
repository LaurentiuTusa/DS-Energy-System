import React from 'react';
import { useNavigate } from 'react-router-dom';

function LogoutButton() {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Clear data from local storage
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('currentUserId');
    localStorage.removeItem('currentUserRole');

    // Redirect to the login page
    navigate('/login');
  };

  return (
    <button onClick={handleLogout}>Logout</button>
  );
}

export default LogoutButton;
