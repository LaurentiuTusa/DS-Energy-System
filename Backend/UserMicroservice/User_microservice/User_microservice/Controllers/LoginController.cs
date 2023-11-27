using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DAL.Repository.Models;
using BLL;
using User_microservice.Services;
using Microsoft.AspNetCore.Hosting.Server;
using static User_microservice.Services.Constants;

namespace User_microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private BLL.UserBLL _userBLL;
        private JwtTokenService _jwtTokenService;
        public LoginController(IConfiguration config)
        {
            _config = config;
            _userBLL = new BLL.UserBLL();
            _jwtTokenService = new JwtTokenService(_config);
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLogin model)
        {
            
            // Find the user by email
            var user = _userBLL.GetUserByEmail(model);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Verify the hashed password using BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (isPasswordValid)
            {
                var token = _jwtTokenService.CreateToken(user);

                var response = new
                {
                    token = token,
                    currentUserId = user.Id,
                    currentUserRole = user.Role
                };
                
                return Ok(response);
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Name))
            {
                return BadRequest("Name, Password and Email are required.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User u = new User();
            u.Name = user.Name;
            u.Email = user.Email;
            u.Password = hashedPassword;
            u.Role = "user";

            int newUserId = _userBLL.AddUser(u);

            // Synchronize the databases by making a request to AddUserId in Device_microservice
            using (HttpClient client = new HttpClient())
            {
                // Make the HTTP POST request to AddUserId endpoint in Device_microservice
                HttpResponseMessage response = client.PostAsync(server + $"/Device/AddUserId?user_id={newUserId}", null).Result;

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    //call the login method if register successful
                    UserLogin userLogin = new UserLogin();
                    userLogin.Email = user.Email;
                    userLogin.Password = user.Password;

                    return Login(userLogin);
                }
                else
                {
                    // Handle synchronization failure
                    return StatusCode((int)response.StatusCode, "Database synchronization failed");
                }
            }
        }
    }
}
