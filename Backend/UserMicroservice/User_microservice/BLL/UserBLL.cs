using DAL.Repository.Models;

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

        public User GetUserByEmail(UserLogin userLogin)
        {
            var data = _userDAL.GetUserByEmail(userLogin.Email);
            if (data == null || !(userLogin.Password.Equals(data.Password))) //email not found or invalid password
            {
                throw new Exception("Invalid email or password");
            }
            return data;
        }

        public void AddUser(User user)
        {
            _userDAL.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            _userDAL.UpdateUser(user);
        }

        public void DeleteUserById(int id)
        {
            _userDAL.DeleteUserById(id);
        }
    }
}