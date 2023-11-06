using DAL.Repository.Models;

namespace BLL
{
    public class DeviceBLL
    {
        private DAL.DeviceDAL _deviceDAL;

        public DeviceBLL()
        {
            _deviceDAL = new DAL.DeviceDAL();
        }

        //Methods
        public List<Device> GetAllDevices()
        {
            return _deviceDAL.GetAllDevices();
        }

        public Device GetDeviceById(int id)
        {
            var data = _deviceDAL.GetDeviceById(id);

            if (data == null)
            {
                throw new Exception("Invalid Id");
            }

            return data;
        }

        public void AddDevice(Device device)
        {
            _deviceDAL.AddDevice(device);
        }

        public void DeleteDeviceById(int id)
        {
            _deviceDAL.DeleteDeviceById(id);
        }

        public void UpdateDevice(Device device)
        {
            _deviceDAL.UpdateDevice(device);
        }

        public List<Device> GetAllDevicesByUserId(int userId)
        {
            return _deviceDAL.GetAllDevicesByUserId(userId);
        }

        public List<User> GetAllUsers()
        {
            return _deviceDAL.GetAllUsers();
        }

        public void AddUserId(User user)
        {
            _deviceDAL.AddUserId(user);
        }

        public void DeleteUserId(int id)
        {
            _deviceDAL.DeleteUserId(id);
        }
    }
}