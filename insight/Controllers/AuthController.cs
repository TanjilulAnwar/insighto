using insight.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace insight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _jwtKey;

        public AuthController(IConfiguration configuration)
        {
            _jwtKey = configuration["jwt:key"];

        }
        [HttpPost("/signup")]
        public IActionResult Signup([FromBody] User user)
        {
            if (DataStore.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("Email already exists.");
            }
            DataStore.Users.Add(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("/login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = DataStore.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }
    }
}
