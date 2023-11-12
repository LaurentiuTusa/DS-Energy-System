import React, {useState, useEffect, Fragment} from 'react';
import Table from 'react-bootstrap/Table';
import 'bootstrap/dist/css/bootstrap.min.css';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import LogoutButton from './LogoutButton';
import { useNavigate } from 'react-router-dom';

const UserDashboard = () => {

  const [description, setDescription] = useState('');
  const [address, setAddress] = useState('');
  const [maxHourlyConsumption, setMaxHourlyConsumption] = useState('');

  const navigate = useNavigate();
  const [data, setData] = useState([]);

  useEffect(() => {
    const currentUserId = localStorage.getItem('currentUserId');
    const currentUserRole = localStorage.getItem('currentUserRole');

    //extract the userId from the URL
    const userIdFromUrl = window.location.pathname.split('/')[2];
    console.log('userIdFromUrl:', userIdFromUrl);

    //if the userId from the URL is not the same as the currentUserId or the role is not user, redirect to the unauthorized page
    if ((userIdFromUrl !== currentUserId) || (currentUserRole !== 'user')) {
      navigate('/unauthorized');
    }
    
    getData();
  }, []);

  const getData = () => {
    const currentUserId = localStorage.getItem('currentUserId');

    axios.get(`https://localhost:7172/Device/GetAllDevicesByUserId?userId=${currentUserId}`)
    .then((result) => {
      setData(result.data);
    })
    .catch((error) => {
      console.log(error)
    })
  }

  const handleSave =() => {

    const url = 'https://localhost:7172/Device/AddDevice';

    const currentUserId = localStorage.getItem('currentUserId');
    const data = {
      "description": description,
      "address": address,
      "maxHourlyConsumption": maxHourlyConsumption,
      "userId": currentUserId
    }

    axios.post(url, data)
    .then((result) =>{
      getData();
      clear();
      toast.success('Device added successfully');
    }).catch((error) => {
      toast.error(error);
    })
  }

  const handleDrop =(id) => {

    if (window.confirm('Are you sure you want to drop this device?') == true) {

      axios.put(`https://localhost:7172/Device/DropDevice?id=${id}`)
      .then((result) => {
        if (result.status === 200) {
          toast.success('Device dropped successfully');
          getData();
        }
      })
      .catch((error) => {
        toast.error(error);
      })
    }
  }

  const clear = () => {
    setDescription('');
    setAddress('');
    setMaxHourlyConsumption('');
  }

  return (
    <Fragment>
      <div>
        <LogoutButton />
      </div>
      <ToastContainer />
      <Container>
        <Row>
          <Col>
            <input type="text" className="form-control" placeholder="Enter Description" 
            value={description} onChange={(e) => setDescription(e.target.value)}
            />
          </Col>
          <Col>
            <input type="text" className="form-control" placeholder="Enter address" 
            value={address} onChange={(e) => setAddress(e.target.value)}
            />
          </Col>
          <Col>
            <input type="number" className="form-control" placeholder="Enter maxHourlyConsumption" 
            value={maxHourlyConsumption} onChange={(e) => setMaxHourlyConsumption(e.target.value)}
            />
          </Col>
          
          <Col>
            <button className="btn btn-primary" onClick={() => handleSave()}>Add</button>
          </Col>
        </Row>
      </Container>

      <br></br>

      <Table striped bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>ID</th>
            <th>Description</th>
            <th>Address</th>
            <th>MaxHourlyConsumption</th>
            <th>UserId</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {
            data && data.length > 0 ?
            data.map((item, index) => {
              return (
                <tr key={index}>
                  <td>{index + 1}</td>
                  <td>{item.id}</td>
                  <td>{item.description}</td>
                  <td>{item.address}</td>
                  <td>{item.maxHourlyConsumption}</td>
                  <td>{item.userId}</td>
                  <td>
                    <button className="btn btn-danger" onClick={() => handleDrop(item.id)} >Drop</button>
                  </td>
                </tr>
              )
            })
            :
            'Loading...'
          }
        </tbody>
      </Table>
    </Fragment>
  )
}

export default UserDashboard;
