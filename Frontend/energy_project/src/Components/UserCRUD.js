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

const UserCRUD = () => {
  
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState('user');

  const [editId, setEditId] = useState('');
  const [editName, setEditName] = useState('');
  const [editEmail, setEditEmail] = useState('');
  const [editPassword, setEditPassword] = useState('');
  const [editRole, setEditRole] = useState('user');

  const [data, setData] = useState([]);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    axios.get('https://localhost:7167/api/User/GetAllUsers')
    .then((result) => {
      setData(result.data);
    })
    .catch((error) => {
      console.log(error)
    })
  }

  const handleEdit =(id) => {
    handleShow();
    axios.get(`https://localhost:7167/api/User/${id}`)
    .then((result) => {
      setEditName(result.data.name);
      setEditEmail(result.data.email);
      setEditPassword(result.data.password);
      setEditRole(result.data.role);
      setEditId(id);
    })
    .catch((error) => {
      console.log(error)
    })
  }

  // const handleDelete =(id) => {
  //   if (window.confirm('Are you sure you want to delete this user?') == true) {
  //     axios.delete(`https://localhost:7167/api/User/DeleteUserById?id=${id}`)
  //     .then((result) => {
  //       if (result.status === 200) {
  //         toast.success('User deleted successfully');
  //         getData();
  //       }
  //     })
  //     .catch((error) => {
  //       toast.error(error);
  //     })
  //   }
  // }

  const handleDelete = (id) => {
    if (window.confirm('Are you sure you want to delete this user?')) {
      // First, send a delete request to the User microservice
      axios
        .delete(`https://localhost:7167/api/User/DeleteUserById?id=${id}`)
        .then((result) => {
          if (result.status === 200) {
            toast.success('User deleted successfully');
            // After successful deletion from the User microservice, execute a delete in the Device microservice
            axios
              .delete(`https://localhost:7172/Device/DeleteUserId?id=${id}`)
              .then((deviceResult) => {
                if (deviceResult.status === 200) {
                  toast.success('Device deleted successfully');
                  getData(); // Refresh data if needed
                }
              })
              .catch((deviceError) => {
                toast.error('Error while deleting userId from device: ' + deviceError);
              });
          }
        })
        .catch((error) => {
          toast.error('Error while deleting user: ' + error);
        });
    }
  };


  const handleUpdate =() => {
    const url = 'https://localhost:7167/api/User/UpdateUser';
    const data = {
      "id": editId,
      "name": editName,
      "email": editEmail,
      "password": editPassword,
      "role": editRole
    }

    axios.put(url, data)
    .then((result) => {
      handleClose();
      getData();
      clear();
      toast.success('User updated successfully');
    }).catch((error) => {
      toast.error(error);
    })
  }

  // const handleSave =() => {
  //   const url = 'https://localhost:7167/api/User/AddUser';
  //   const data = {
  //     "name": name,
  //     "email": email,
  //     "password": password,
  //     "role": role
  //   }

  //   axios.post(url, data)
  //   .then((result) =>{
  //     getData();
  //     clear();
  //     toast.success('User added successfully');
  //   }).catch((error) => {
  //     toast.error(error);
  //   })
  // }

  const handleSave = () => {
    const userMicroserviceUrl = 'https://localhost:7167/api/User/AddUser';
    const deviceMicroserviceUrl = 'https://localhost:7172/Device/AddUserId';
  
    const userData = {
      "name": name,
      "email": email,
      "password": password,
      "role": role
    };
  
    axios.post(userMicroserviceUrl, userData)
      .then((userResult) => {
        const newUserID = userResult.data.id; // Assuming the response contains the user's ID
  
        // Create data for the second microservice
        const userDataForDeviceMicroservice = {
          "userId": newUserID // Pass the newly created user's ID
        };
  
        // Make a POST request to the other microservice
        axios.post(deviceMicroserviceUrl, userDataForDeviceMicroservice)
          .then((deviceResult) => {
            getData(); // Refresh data if needed
            clear();
            toast.success('User added successfully');
            toast.success('Data added to device microservice');
          })
          .catch((deviceError) => {
            toast.error('Error while adding data to the device microservice: ' + deviceError);
          });
      })
      .catch((userError) => {
        toast.error('Error while adding user: ' + userError);
      });
  };

  const clear = () => {
    setName('');
    setEmail('');
    setPassword('');
    setRole('');
    setEditName('');
    setEditEmail('');
    setEditPassword('');
    setEditRole('');
    setEditId('');
  }

  const handleRoleChange = (e) => {
    if (e.target.checked) {
      setRole('admin');
    } else {
      setRole('user');
    }
  }

  const handleEditRoleChange = (e) => {
    if (e.target.checked) {
      setEditRole('admin');
    } else {
      setEditRole('user');
    }
  }

  //This is the code for the user ADD
  return (
    <Fragment>
      <ToastContainer />
      <Container>
        <Row>
          <Col>
            <input type="text" className="form-control" placeholder="Enter Name" 
            value={name} onChange={(e) => setName(e.target.value)}
            />
          </Col>
          <Col>
            <input type="text" className="form-control" placeholder="Enter email" 
            value={email} onChange={(e) => setEmail(e.target.value)}
            />
          </Col>
          <Col>
            <input type="text" className="form-control" placeholder="Enter password" 
            value={password} onChange={(e) => setPassword(e.target.value)}
            />
          </Col>
          <Col>
            <input type="checkbox" 
            checked={role === 'admin' ? true : false}
            onChange={(e) => handleRoleChange(e)} value={role === 'admin' ? 'checked' : 'unchecked'}// treu : false
            />
            <label>Admin</label>
          </Col>
          <Col>
            <button className="btn btn-primary" onClick={() => handleSave()}>Submit</button>
          </Col>
        </Row>
      </Container>

      <br></br>
      
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Password</th>
            <th>Role</th>
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
                  <td>{item.name}</td>
                  <td>{item.email}</td>
                  <td>{item.password}</td>
                  <td>{item.role}</td>
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
          <Modal.Title>Modify User</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          
          <Row>
            <Col>
              <input type="text" className="form-control" placeholder="Enter Name" 
              value={editName} onChange={(e) => setEditName(e.target.value)}
              />
            </Col>
            <Col>
              <input type="text" className="form-control" placeholder="Enter email" 
              value={editEmail} onChange={(e) => setEditEmail(e.target.value)}
              />
            </Col>
            <Col>
              <input type="text" className="form-control" placeholder="Enter password" 
              value={editPassword} onChange={(e) => setEditPassword(e.target.value)}
              />
            </Col>
            <Col>
              <input type="checkbox" 
              checked={editRole === 'admin' ? true : false}
              onChange={(e) => handleEditRoleChange(e)} value={editRole === 'admin' ? 'checked' : 'unchecked'}
              />
              <label>Admin</label>
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

export default UserCRUD;
