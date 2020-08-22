using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopPrototype
{
    public interface IJwtAuthenticationManager
    {
        public string Authenticate(string username, string password);
    }
}
