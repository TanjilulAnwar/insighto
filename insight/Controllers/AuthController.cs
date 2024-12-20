using insight.DataContext;
using insight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace insight.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _jwtKey;

        public AuthController(IConfiguration configuration)
        {
            _jwtKey = configuration["jwt:key"];

        }
        [HttpPost]
        public IActionResult Signup([FromBody] User user)
        {
            try
            {
                if (DataStore.Users.Any(u => u.Email == user.Email))
                {
                    return Ok(new GlobalResponse("Email already exists!", false));
                }
                DataStore.Users.Add(user);
                return Ok(new GlobalResponse("Successfully Signed up, Please log in to continue", true));
            }
            catch
            {
                return Ok(new GlobalResponse("Sign up Failed",false));
            }
      
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserVm login)
        {
            try
            {
                var user = DataStore.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
                if (user == null)
                {
                    return Ok(new GlobalResponse("Invalid credentials!", false));

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
                return Ok(new GlobalResponse("Login Successful!", true, new { Name = user.Name, Token = tokenHandler.WriteToken(token) }));
            }
            catch
            {
                return Ok(new GlobalResponse("Login Failed!", false));
            }
         
        }
    }
}
