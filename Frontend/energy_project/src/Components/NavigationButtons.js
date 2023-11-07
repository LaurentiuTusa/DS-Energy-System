import { useNavigate } from 'react-router-dom';

function NavigationButtons() {
  const navigate = useNavigate();

  const goToUserCRUD = () => {
    navigate('/adminUserCRUD');
  };

  const goToDeviceCRUD = () => {
    navigate('/adminDeviceCRUD');
  };

  return (
    <div>
      <button onClick={goToUserCRUD}>User CRUD</button>
      <button onClick={goToDeviceCRUD}>Device CRUD</button>
    </div>
  );
}

export default NavigationButtons;
