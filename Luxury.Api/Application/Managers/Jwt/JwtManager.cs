using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Luxury.Api.Application.Managers.Jwt
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _config;
        private SymmetricSecurityKey _key;

        public JwtManager(IConfiguration config)
        {
            _config = config;
        }

        public string GetToken()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claim = new[]
            {
                new Claim("JWT_LuxuryAPI","Luxury")
            };

            var Token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claim,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }


    }
}
