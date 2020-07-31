using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using WebApiProject.Model;

namespace WebApiProject.Services
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public interface IUserService
    {
        LoginSuccessModel Login(LoginModel model);
        IEnumerable<User> GetAll();
    }

    public class UserService : IUserService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "tejas", Password = "123", Roles = new[]{ "member"} },
            new User { Id = 2, Username = "admin", Password = "admin", Roles = new[]{ "admin"} }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public LoginSuccessModel Login(LoginModel model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = CreateBearerToken(user);

            return new LoginSuccessModel()
            {
                Id = user.Id,
                Username = user.Username,
                Token = token
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        private string CreateBearerToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var claims = user.Roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaim = new Claim(ClaimTypes.Name, user.Id.ToString());
            claims.Add(userClaim);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
