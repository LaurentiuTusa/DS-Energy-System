using User_microservice.Repository.Models;
using User_microservice.Repository;

namespace BLL
{
    public class UserBLL
    {
        private DAL.UserDAL _userDAL;
        public UserBLL()
        {
            _userDAL = new DAL.UserDAL();
        }

        // Methods
        public List<User> GetAllUsers()
        {
            return _userDAL.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            var data = _userDAL.GetUserById(id);

            if (data == null)
            {
                throw new Exception("Invalid Id");
            }

            return data;
        }

        public void AddUser(User user)
        {
            _userDAL.AddUser(user);
        }

        public void DeleteUserById(int id)
        {
            _userDAL.DeleteUserById(id);
        }
    }
}