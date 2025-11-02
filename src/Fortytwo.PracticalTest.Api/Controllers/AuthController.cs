
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Fortytwo.PracticalTest.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(IConfiguration cfg) : ControllerBase
    {
        public record LoginRequest(string Username, string Password);

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (req.Username != "admin" || req.Password != "password")
                return Unauthorized();

            var issuer = cfg["Jwt:Issuer"] ?? "fortytwo";
            var audience = cfg["Jwt:Audience"] ?? "fortytwo-clients";
            var key = cfg["Jwt:Key"] ?? "dev-secret-key-change-me-0123456789ABCD";
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: new[] { new Claim(ClaimTypes.Name, req.Username) },
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = jwt });
        }
    }
}
