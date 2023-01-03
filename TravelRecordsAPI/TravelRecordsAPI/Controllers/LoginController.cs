using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelRecordsAPI.Models;
using TravelRecordsAPI.Models.Dto;

namespace TravelRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly CoreDbContext _context;

        public LoginController(IConfiguration config, CoreDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserDto userLogin)
        {
            var user = _context.Users.FirstOrDefault(e => e.Username == userLogin.Username &&
                                                            e.Password == userLogin.Password);
            if(user!=null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            user = _context.Users.FirstOrDefault(e => e.Username == userLogin.Username);

            if(user!=null)
            {
                return NotFound("Wrong password");
            }
            return NotFound("User not found");
        }

        private String GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Sid,user.UserId.ToString()),
                new Claim(ClaimTypes.Role,"User")
            };

            var token=new JwtSecurityToken(claims:claims, 
                                            expires:DateTime.Now.AddMinutes(15),
                                            signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
