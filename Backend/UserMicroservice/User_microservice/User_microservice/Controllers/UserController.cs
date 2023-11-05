using System.Security.Claims;
using DAL.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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
        [Route("Admins")]
        //[Authorize(Policy = "IsAdmin")]
        //[Authorize(Roles = "admin")] //sau cu a mic
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser.Name}, you are an {currentUser.Role}");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    Name = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }



        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers() 
        {
            //Console.WriteLine("am trecut pepciic");
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

        [HttpPut]
        [Route("UpdateUser")]
        public void UpdateUser([FromBody] User user)
        {
            _userBLL.UpdateUser(user);
        }

        [HttpDelete]
        [Route("DeleteUserById")]
        public void DeleteUserById(int id)
        {
            _userBLL.DeleteUserById(id);
        }
    }
}
