using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace DapperNetCore8_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiTokenController : ControllerBase
    {
        [Route("get")]
        [Authorize]
        [HttpGet]
        public string Get()
        {
            return "RAMBO";
        }
        [Route("loginol")]
        [HttpGet]
        public string login(string username, string role)
        {
            return "Bearer " + GenerateToken(username, role);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string GenerateToken(string username, string role)
        {
            var claims = new List<Claim>{
new Claim(ClaimTypes.Name, username),
new Claim(ClaimTypes.Role, role)
};
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-key"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(10),
            signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
