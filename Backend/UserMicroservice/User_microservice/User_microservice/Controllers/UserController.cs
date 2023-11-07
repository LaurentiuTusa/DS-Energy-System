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
        [Authorize(Roles = "admin")]
        public IActionResult AdminEndPoint()
        {
            return Ok($"Hi admin, you are an allowed");
        }

/*        private User GetCurrentUser()
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
        }*/



        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers() 
        {
            return _userBLL.GetAllUsers();
        }

        /*[HttpGet]
        [Route("GetUserById")]*/
        [HttpGet("{id}", Name = "GetUserById")]
        public User GetUserById(int id)
        {
            return _userBLL.GetUserById(id);
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            int newUserId = _userBLL.AddUser(user);

            // Generate the URL for the "GetUserById" route
            var url = Url.Link("GetUserById", new { id = newUserId });

            // Return a response that includes the URL
            if (url != null)
            {
                return Created(url, new { id = newUserId });
            }
            else
            {
                return BadRequest("URL generation failed");
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        [Authorize(Roles = "admin")]
        public void UpdateUser([FromBody] User user)
        {
            _userBLL.UpdateUser(user);
        }

        [HttpDelete]
        [Route("DeleteUserById")]
        [Authorize(Roles = "admin")]
        public void DeleteUserById(int id)
        {
            _userBLL.DeleteUserById(id);
        }
    }
}
