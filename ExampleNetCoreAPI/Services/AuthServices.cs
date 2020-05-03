using ExampleNetCoreAPI.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExampleNetCoreAPI.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
    }

    public class AuthService : IAuthService
    {
        public AuthService()
        {
        }
        public User Authenticate(string username, string password)
        {

            #region User kontrolü yapıyoruz şimdilik statik liste oluşturduk
            IList<User> userlist = new List<User>();
            userlist.Add(new User { Username = "user1", Password = "123" });
            userlist.Add(new User { Username = "user2", Password = "abc" });

            User user = userlist.Where(x => x.Username == username).Where(x => x.Password == password).SingleOrDefault();
            #endregion
                    
            #region JWT Token'ımızı oluşturuyoruz
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789..");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "false",
                Audience = "false",
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            #endregion

            user.Token = tokenHandler.WriteToken(token);
            user.Password = null;

            return user;
        }
    }
}
