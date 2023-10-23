using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_microservice.Repository;
using User_microservice.Repository.Models;

namespace User_microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private BLL.UserBLL _userBLL;
        public UserController()
        {
            _userBLL = new BLL.UserBLL();
        }

        // Methods

        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers() 
        {
            return _userBLL.GetAllUsers();
        }

        [HttpGet]
        [Route("GetUserById")]
        public User GetUserById(int id)
        {
            return _userBLL.GetUserById(id);
        }

        [HttpPost]
        [Route("AddUser")]
        public void AddUser([FromBody] User user)
        {
            _userBLL.AddUser(user);
        }

        [HttpDelete]
        [Route("DeleteUserById")]
        public void DeleteUserById(int id)
        {
            _userBLL.DeleteUserById(id);
        }
    }
}
