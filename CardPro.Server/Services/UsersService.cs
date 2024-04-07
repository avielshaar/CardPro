using CardPro.Server.Models;
using CardPro.Server.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardPro.Server.Services
{
    public class UsersService : IUsersService
    {
        private readonly JwtSettings jwtSettings;
        private readonly List<User> users;

        public UsersService(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
            this.users = new List<User>()
            {
                new User()
                {
                    Email = "admin@gmail.com",
                    Password = "12345678"
                }
            };
        }

        public bool Login(User user)
        {
            return users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password) != null;
        }

        public string GenerateJwtToken(User user)
        {
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: new List<Claim>() { new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.Role, "Admin") },
                expires: DateTime.UtcNow.Add(jwtSettings.AbsoluteExpirationRelativeToNow),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)), SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
