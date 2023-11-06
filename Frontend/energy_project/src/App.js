import './App.css';
import React from 'react';
import { BrowserRouter as Router, Route, Routes, useNavigate} from 'react-router-dom';
import UserCRUD from './Components/UserCRUD';
import DeviceCRUD from './Components/DeviceCRUD';
import Login from './Components/Login';

function App() {
  return (
    
      <Router>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/adminUserCRUD" element={<UserCRUD />} />
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




// import React, { useState } from 'react';
// import './App.css';
// import UserCRUD from './Components/UserCRUD';
// import DeviceCRUD from './Components/DeviceCRUD';

// function App() {
//   const [showUserCRUD, setShowUserCRUD] = useState(true);

//   const toggleComponent = () => {
//     setShowUserCRUD(!showUserCRUD);
//   };

//   return (
//     <div className="App">
//       <div>
//         <button onClick={toggleComponent}>
//           {showUserCRUD ? 'Show Device CRUD' : 'Show User CRUD'}
//         </button>
//       </div>
//       {showUserCRUD ? <UserCRUD /> : <DeviceCRUD />}
//     </div>
//   );
// }

// export default App;

//https://localhost:7167/api/Login/Register
//https://localhost:7167/api/Login/Login
