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
import NavigationButtons from './NavigationButtons';
import LogoutButton from './LogoutButton';

const DeviceCRUD = () => {

  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [description, setDescription] = useState('');
  const [address, setAddress] = useState('');
  const [maxHourlyConsumption, setMaxHourlyConsumption] = useState('');
  const [userId, setUserId] = useState('');

  const [editId, setEditId] = useState('');
  const [editDescription, setEditDescription] = useState('');
  const [editAddress, setEditAddress] = useState('');
  const [editMaxHourlyConsumption, setEditMaxHourlyConsumption] = useState('');
  const [editUserId, setEditUserId] = useState('');

  const [data, setData] = useState([]);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {

    axios.get('https://localhost:7172/Device/GetAllDevices')
    .then((result) => {
      setData(result.data);
    })
    .catch((error) => {
      console.log(error)
    })
  }

  const handleEdit =(id) => {

    handleShow();

    axios.get(`https://localhost:7172/Device/GetDeviceById?id=${id}`)
    .then((result) => {
      setEditDescription(result.data.description);
      setEditAddress(result.data.address);
      setEditMaxHourlyConsumption(result.data.maxHourlyConsumption);
      setEditUserId(result.data.userId);
      setEditId(id);
    })
    .catch((error) => {
      console.log(error)
    })
  }

  const handleDelete =(id) => {

    if (window.confirm('Are you sure you want to delete this device?') == true) {

      const jwtToken = localStorage.getItem('jwtToken');
      const url = `https://localhost:7172/Device/DeleteDeviceById?id=${id}`;

      axios.delete(url, {
        headers: {
          Authorization: `Bearer ${jwtToken}`,
        }
      })
      .then((result) => {
        if (result.status === 200) {
          toast.success('Device deleted successfully');
          getData();
        }
      })
      .catch((error) => {
        toast.error(error);
      })
    }
  }

  const handleUpdate =() => {

    const jwtToken = localStorage.getItem('jwtToken');

    const url = 'https://localhost:7172/Device/UpdateDevice';
    const data = {
      "id": editId,
      "description": editDescription,
      "address": editAddress,
      "maxHourlyConsumption": editMaxHourlyConsumption,
      "userId": editUserId
    }

    axios.put(url, data, {
      headers: {
        Authorization: `Bearer ${jwtToken}`,
      }
    })
      .then((result) => {
        handleClose();
        getData();
        clear();
        toast.success('Device updated successfully');
      })
        .catch((error) => {
          toast.error(error);
      })
  }

  const handleSave =() => {

    const url = 'https://localhost:7172/Device/AddDevice';
    const data = {
      "description": description,
      "address": address,
      "maxHourlyConsumption": maxHourlyConsumption,
      "userId": userId
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

  const clear = () => {
    setDescription('');
    setAddress('');
    setMaxHourlyConsumption('');
    setUserId('');
    setEditDescription('');
    setEditAddress('');
    setEditMaxHourlyConsumption('');
    setEditUserId('');
    setEditId('');
  }

  //if the userId field is left empty, set the value to null
  const handleUserIdChange = (e) => {
    if (e.target.value === '') {
      setUserId(null);
    } else {
      setUserId(e.target.value);
    }
  }

  const handleEditUserIdChange = (e) => {
    if (e.target.value === '') {
      setEditUserId(null);
    } else {
      setEditUserId(e.target.value);
    }
  }

  //Device ADD
  return (
    <Fragment>
      <div>
        <NavigationButtons />
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
            <input type="text" className="form-control" placeholder="Enter userId"
            //value={userId} onChange={(e) => setUserId(e.target.value)}// consider using handleUserIdChange() instead
            value={userId} onChange={(e) => handleUserIdChange(e)}
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
                    <button className="btn btn-success" onClick={() => handleEdit(item.id)} >Edit</button> &nbsp;
                    <button className="btn btn-danger" onClick={() => handleDelete(item.id)} >Delete</button>
                  </td>
                </tr>
              )
            })
            :
            'Loading...'
          }
        </tbody>
      </Table>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Modify Device</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          
          <Row>
            <Col>
              <input type="text" className="form-control" placeholder="Enter Description" 
              value={editDescription} onChange={(e) => setEditDescription(e.target.value)}
              />
            </Col>
            <Col>
              <input type="text" className="form-control" placeholder="Enter address" 
              value={editAddress} onChange={(e) => setEditAddress(e.target.value)}
              />
            </Col>
            <Col>
              <input type="number" className="form-control" placeholder="Enter maxHourlyConsumption" 
              value={editMaxHourlyConsumption} onChange={(e) => setEditMaxHourlyConsumption(e.target.value)}
              />
            </Col>
            <Col>
            <input type="text" className="form-control" placeholder="Enter userId"
            //value={editUserId} onChange={(e) => setEditUserId(e.target.value)}
            value={editUserId} onChange={(e) => handleEditUserIdChange(e)}
              />
            </Col>
          </Row>

        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdate}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>

    </Fragment>
  )
}

export default DeviceCRUD;
