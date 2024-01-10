import './App.css';
import React from 'react';
import { BrowserRouter as Router, Route, Routes, useNavigate} from 'react-router-dom';
import UserCRUD from './Components/UserCRUD';
import DeviceCRUD from './Components/DeviceCRUD';
import Login from './Components/Login';
import UserDashboard from './Components/UserDashboard';
import PrivateRoute from './Components/PrivateRoute';
import Unauthorized from './Components/Unauthorized';
import ChatService from './Components/ChatService';

function App() {
  return (
      <Router>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/adminUserCRUD/:userId" element={<UserCRUD />} />
          <Route path="/adminDeviceCRUD/:userId" element={<DeviceCRUD />} />
          <Route path="/userDashboard/:userId" element={<UserDashboard />} />
          <Route path="/chatService" element={<ChatService />} />
          <Route path="/unauthorized" element={<Unauthorized />} />
          <Route index element={<NavigateToLogin />} /> {/* Redirect to /login if no route matches */}
      </Routes>
    </Router>
  );
}

function NavigateToLogin() {
  const navigate = useNavigate();

  React.useEffect(() => {
    navigate('/login');
  }, [navigate]);

  return null;
}

export default App;
