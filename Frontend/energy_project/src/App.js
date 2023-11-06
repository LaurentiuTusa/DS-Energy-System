// import './App.css';
// import UserCRUD from './Components/UserCRUD';
// import DeviceCRUD from './Components/DeviceCRUD';

// function App() {
//   return (
//     <div className="App">
//       <DeviceCRUD />

//     </div> 
//   );
// }

// export default App;




import React, { useState } from 'react';
import './App.css';
import UserCRUD from './Components/UserCRUD';
import DeviceCRUD from './Components/DeviceCRUD';

function App() {
  const [showUserCRUD, setShowUserCRUD] = useState(true);

  const toggleComponent = () => {
    setShowUserCRUD(!showUserCRUD);
  };

  return (
    <div className="App">
      <div>
        <button onClick={toggleComponent}>
          {showUserCRUD ? 'Show Device CRUD' : 'Show User CRUD'}
        </button>
      </div>
      {showUserCRUD ? <UserCRUD /> : <DeviceCRUD />}
    </div>
  );
}

export default App;
