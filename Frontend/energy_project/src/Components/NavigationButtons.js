import { useNavigate } from 'react-router-dom';

function NavigationButtons() {
  const navigate = useNavigate();
  const currentUserRole = localStorage.getItem('currentUserRole');

  const goToUserCRUD = () => {
    const currentUserId = localStorage.getItem('currentUserId');
    navigate(`/adminUserCRUD/${currentUserId}`);
  };

  const goToDeviceCRUD = () => {
    const currentUserId = localStorage.getItem('currentUserId');
    navigate(`/adminDeviceCRUD/${currentUserId}`);
  };

  const goToChatService = () => {
    navigate(`/chatService`);
  };

  return (//for users with role admin display the buttons for user CRUD, device CRUD and chat service. But for users with role user display only the button for chat service
    <div>

      {currentUserRole === 'admin' ? (
        // Content for users with the 'admin' role
        <>
          <button onClick={goToUserCRUD}>User CRUD</button>
          <button onClick={goToDeviceCRUD}>Device CRUD</button>
          <button onClick={goToChatService}>Chat</button>
        </>
      ) : currentUserRole === 'user' ? (
        // Content for users with the 'user' role
        <button onClick={goToChatService}>Chat</button>
      ) : (
        // Default content (e.g., if role is not defined)
        <p>No buttons to display</p>
      )}
    </div>
  );
}

export default NavigationButtons;
