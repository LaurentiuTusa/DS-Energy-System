import { useNavigate } from 'react-router-dom';

function NavigationButtons() {
  const navigate = useNavigate();

  const goToUserCRUD = () => {
    const currentUserId = localStorage.getItem('currentUserId');
    navigate(`/adminUserCRUD/${currentUserId}`);
  };

  const goToDeviceCRUD = () => {
    const currentUserId = localStorage.getItem('currentUserId');
    navigate(`/adminDeviceCRUD/${currentUserId}`);
  };

  return (
    <div>
      <button onClick={goToUserCRUD}>User CRUD</button>
      <button onClick={goToDeviceCRUD}>Device CRUD</button>
    </div>
  );
}

export default NavigationButtons;
