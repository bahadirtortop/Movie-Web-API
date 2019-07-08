using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieWebAPI.Infrastructure.AppSettings;
using MovieWebAPI.Model.User;

namespace MovieWebAPI.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous()]
    public class TokenController : ControllerBase
    {
        private IConfiguration _configuration;
        Jwt _jwt;
        Authentication _authentication;
        public TokenController(IConfiguration configuration,IOptions<Jwt> jwtOptions, IOptions<Authentication> authenticationOptions)
        {
            _configuration = configuration;
            _jwt = jwtOptions.Value;
            _authentication = authenticationOptions.Value;
        }

        [HttpPost]
        public IActionResult GetToken([FromBody]UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.Username==_authentication.UserName&&user.Password == _authentication.Password)
                {
                    return Ok(GenerateToken(user.Username));
                }
            }
            return Unauthorized();
        }
        private string GenerateToken(string userName)
        {
            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.UniqueName,userName)
            };

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
