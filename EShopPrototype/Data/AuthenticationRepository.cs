using EShopPrototype.Data.Interfaces;
using SharedItems.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopPrototype.Data
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDBContext _appDBContext;

        public AuthenticationRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public User GetUserByName(string name)
        {
            return _appDBContext.Users.FirstOrDefault(x => x.Username == name);
        }

        public bool Register(User user)
        {
            try
            {
                _appDBContext.Users.Add(user);
                _appDBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        
    }
}
