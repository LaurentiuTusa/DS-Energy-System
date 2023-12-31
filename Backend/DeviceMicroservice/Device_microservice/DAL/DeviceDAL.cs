﻿using DAL.Repository;
using DAL.Repository.Models;

namespace DAL
{
    public class DeviceDAL
    {
        public List<Device> GetAllDevices()
        {
            var db = new DeviceDbContext();
            return db.Devices.ToList();
        }

        public Device GetDeviceById(int id)
        {
            var db = new DeviceDbContext();
            Device u = new Device();

            u = db.Devices.FirstOrDefault(u => u.Id == id);

            return u;
        }

        public void AddDevice(Device device)
        {
            var db = new DeviceDbContext();
            db.Devices.Add(device);
            db.SaveChanges();
        }

        public void UpdateDevice(Device device)
        {
            var db = new DeviceDbContext();
            Device u = new Device();
            u = db.Devices.FirstOrDefault(u => u.Id == device.Id);
            if (u == null)
            {
                throw new Exception("Device to update Not Found");
            }
            u.Description = device.Description;
            u.Address = device.Address;
            u.MaxHourlyConsumption = device.MaxHourlyConsumption;
            u.UserId = device.UserId;
            db.SaveChanges();
        }

        public void DropDevice(int id)
        {
            var db = new DeviceDbContext();
            Device u = new Device();
            u = db.Devices.FirstOrDefault(u => u.Id == id);
            if (u == null)
            {
                throw new Exception("Device to drop Not Found");
            }

            u.UserId = null;
            db.SaveChanges();
        }

        public void DeleteDeviceById(int id)
        {
            var db = new DeviceDbContext();
            Device u = new Device();
            u = db.Devices.FirstOrDefault(u => u.Id == id);

            if (u == null)
            {
                throw new Exception("Device to delete Not Found");
            }

            db.Devices.Remove(u);
            db.SaveChanges();
        }

        public List<Device> GetAllDevicesByUserId(int userId)
        {
            var db = new DeviceDbContext();
            return db.Devices.Where(u => u.UserId == userId).ToList();
        }

        public List<User> GetAllUsers()
        {
            var db = new DeviceDbContext();
            return db.Users.ToList();
        }

        public void AddUserId (User user)
        {
            var db = new DeviceDbContext();
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void DeleteUserId(int id)
        {
            var db = new DeviceDbContext();
            User u = new User();
            u = db.Users.FirstOrDefault(u => u.UserId == id);
            if (u == null)
            {
                throw new Exception("UserId to delete Not Found");
            }
            db.Users.Remove(u);
            db.SaveChanges();
        }

    }
}