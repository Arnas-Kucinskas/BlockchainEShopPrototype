using SharedItems.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopPrototype.Data.Interfaces
{
    public interface IAuthenticationRepository
    {
        public bool Register(User user);
        public User GetUserByName(string name);
    }
}
