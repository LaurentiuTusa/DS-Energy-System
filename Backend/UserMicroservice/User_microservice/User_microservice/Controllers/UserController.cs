using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
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

            // Synchronize the databases by making a request to AddUserId in Device_microservice
            using (HttpClient client = new HttpClient())
            {
                // Make the HTTP POST request to AddUserId endpoint in Device_microservice
                HttpResponseMessage response = client.PostAsync($"https://localhost:7172/Device/AddUserId?user_id={newUserId}", null).Result;
            
                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
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
                else
                {
                    // Handle synchronization failure
                    return StatusCode((int)response.StatusCode, "Database synchronization failed");
                }
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
        public IActionResult DeleteUserById(int id)
        {
            _userBLL.DeleteUserById(id);

            // Retrieve the JWT token from the request headers
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            using (HttpClient client = new HttpClient())
            {
                // Set the authorization header with the JWT token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Make the HTTP POST request to AddUserId endpoint in Device_microservice
                HttpResponseMessage response = client.DeleteAsync($"https://localhost:7172/Device/DeleteUserId?id={id}").Result;

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "UserId not deleted from Device db");
                }
            }
        }
    }
}
