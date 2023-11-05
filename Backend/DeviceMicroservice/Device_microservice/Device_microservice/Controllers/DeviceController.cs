using DAL.Repository.Models;
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

        [HttpDelete]
        [Route("DeleteDeviceById")]
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
    }
}