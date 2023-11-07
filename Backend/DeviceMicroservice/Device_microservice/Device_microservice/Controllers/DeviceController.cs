using System.Data;
using DAL.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Device_microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        private BLL.DeviceBLL _deviceBLL;

        public DeviceController()
        {
            _deviceBLL = new BLL.DeviceBLL();
        }

        //Methods

        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "admin")]
        public IActionResult AdminEndPoint()
        {
            return Ok($"Hi admin, you are allowed");
        }

        [HttpGet]
        [Route("GetAllDevices")]
        public List<Device> GetAllDevices()
        {
            return _deviceBLL.GetAllDevices();
        }

        [HttpGet]
        [Route("GetDeviceById")]
        public Device GetDeviceById(int id)
        {
            return _deviceBLL.GetDeviceById(id);
        }

        [HttpPost]
        [Route("AddDevice")]
        public void AddDevice([FromBody] Device device)
        {
            _deviceBLL.AddDevice(device);
        }

        [HttpPut]
        [Route("UpdateDevice")]
        [Authorize(Roles = "admin")]
        public void UpdateDevice([FromBody] Device device)
        {
            _deviceBLL.UpdateDevice(device);
        }

        [HttpPut]
        [Route("DropDevice")]
        public void DropDevice(int id)
        {
            _deviceBLL.DropDevice(id);
        }

        [HttpDelete]
        [Route("DeleteDeviceById")]
        [Authorize(Roles = "admin")]
        public void DeleteDeviceById(int id)
        {
            _deviceBLL.DeleteDeviceById(id);
        }

        [HttpGet]
        [Route("GetAllDevicesByUserId")]
        public List<Device> GetAllDevicesByUserId(int userId)
        {
            return _deviceBLL.GetAllDevicesByUserId(userId);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return _deviceBLL.GetAllUsers();
        }

        [HttpPost]
        [Route("AddUserId")]
        public void AddUserId([FromBody] User user)
        {
            _deviceBLL.AddUserId(user);
        }

        [HttpDelete]
        [Route("DeleteUserId")]
        [Authorize(Roles = "admin")]
        public void DeleteUserId(int id)
        {
            _deviceBLL.DeleteUserId(id);
        }
    }
}