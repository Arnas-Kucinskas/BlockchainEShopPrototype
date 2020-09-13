using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EShopPrototype.Data.Interfaces;
using SharedItems.Models.Authentication;

namespace EShopPrototype.Services
{
    public class PasswordManager
    {
        private readonly IConfiguration _configuration;
        private readonly string salt;
        private readonly IAuthenticationRepository _authenticationRepository;

        public PasswordManager(IConfiguration configuration, IAuthenticationRepository authenticationRepository )
        {
            _configuration = configuration;
            salt = _configuration.GetSection("Config:Salt").Value;
            _authenticationRepository = authenticationRepository;
        }
        public string GeneratePasswordHash(string password)
        {
            return ComputePassword(password, salt);

        }

        public bool ValidateHashedPassword(string username, string password)
        {
            User user = _authenticationRepository.GetUserByName(username);

            string hashedPassword = ComputePassword(password, salt);
            if (hashedPassword == user.Password)
            {
                return true;
            }
            return false;
        }

        private static String ComputePassword(string password, string salt)
        {
            string saltedPassword = password + salt;

            StringBuilder Sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(saltedPassword));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
