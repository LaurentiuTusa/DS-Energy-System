using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using DAL.Repository.Models;
using BLL;

namespace User_microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private BLL.UserBLL _userBLL;
        public LoginController(IConfiguration config)
        {
            _config = config;
            _userBLL = new BLL.UserBLL();
        }

/*        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("user not found");
        }*/

        // To generate token
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Name),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                //expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //To authenticate user
        private User Authenticate(UserLogin userLogin)
        {
            var currentUser = _userBLL.GetUserByEmail(userLogin);

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
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

            _userBLL.AddUser(u);
            return Ok("Registration successful.");
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
                return Ok("Login successful.");
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }
    }
}
