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
        public void AddUserId([FromQuery] int user_id)
        {
            User user = new User();
            user.UserId = user_id;
            _deviceBLL.AddUserId(user);
        }

        [HttpDelete]
        [Route("DeleteUserId")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteUserId(int id)
        {
            try
            {
                _deviceBLL.DeleteUserId(id);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error deleting user with ID {id}: {ex.Message}");

                // Return an appropriate error response to the client
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}