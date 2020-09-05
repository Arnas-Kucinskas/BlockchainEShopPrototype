using EShopPrototype.Models;
using EShopPrototype.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EShopPrototype.Data
{
    public class BasketRepository
    {
        private readonly AppDBContext _appDBContext;

        public BasketRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void AddProductToBasket(Basket basket)
        {

            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }
            _appDBContext.Baskets.Add(basket);
            _appDBContext.SaveChanges();
        }

        public List<Basket> GetMyBasket(int id)
        {
            List<Basket> basket = _appDBContext.Baskets
                .Where(x => x.UserId == id)
                .Include(p => p.Product)
                .Include(u => u.User)
                .ToList();
            

            //return _appDBContext.Baskets.Where<>
            return null;
        }
    }
}
