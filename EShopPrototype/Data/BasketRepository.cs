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
        private readonly BlockchainRepository _blockchainRepository;
        public BasketRepository(AppDBContext appDBContext, BlockchainRepository blockchainRepository)
        {
            _appDBContext = appDBContext;
            _blockchainRepository = blockchainRepository;
        }

        public  void AddProductToBasket(Basket basket)
        {

            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }
            _appDBContext.Baskets.Add(basket);
            _appDBContext.SaveChanges();
        }

        public async Task<List<Basket>> GetMyBasket(int id)
        {
            List<Basket> basket = _appDBContext.Baskets
                .Where(x => x.UserId == id)
                .Include(p => p.Product)
                .ToList();
            foreach (var item in basket)
            {
                //item.Product.Price = _blockchainRepository.GetProductPrice(item.Product.Id);
                item.Product.Price = await _blockchainRepository.GetProductPrice(item.Product.Id);
            }

            return basket;
        }
    }
}
