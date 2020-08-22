using EShopPrototype.Data;
using EShopPrototype.Data.Interfaces;
using EShopPrototype.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Resources;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShopPrototype
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly Dictionary<string, string> users = new Dictionary<string, string>
        {
            {"test1" , "password1" },
            {"test2" , "password2" }
        };
        private string _key;
        private readonly PasswordManager _passwordManager;

        //private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IConfiguration _configuration;

        public JwtAuthenticationManager(IConfiguration configuration, PasswordManager passwordManager)
        {

            _configuration = configuration;
            _key = _configuration.GetSection("Config:Key").Value;
            _passwordManager = passwordManager;
            //_authenticationRepository = authenticationRepository;
        }
        public string Authenticate(string username, string password)
        {
            bool passwordValid = _passwordManager.ValidateHashedPassword( username,  password);
            if (!passwordValid)
            {
                return null;
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
