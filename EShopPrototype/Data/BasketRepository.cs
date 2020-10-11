using SharedItems.Models;
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

        public  void AddorUpdateProductInBasket(Basket basket)
        {
            if (basket == null)
            {
                throw new ArgumentNullException(nameof(basket));
            }

            Basket product = _appDBContext.Baskets
                .Where(x => x.UserId == basket.UserId)
                .Where(x => x.ProductId == basket.ProductId)
                .FirstOrDefault();

            if (product == null)
            {
                _appDBContext.Baskets.Add(basket);
            }
            else
            {
                product.ProductQuanity += basket.ProductQuanity;
                _appDBContext.Update(product);

            }
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

        public void UpdateQuanityt(Basket basketProduct)
        {
            _appDBContext.Update(basketProduct);
            _appDBContext.SaveChanges();
        }

        public void DeleteBasketProduct(Basket basketProduct)
        {
            if (basketProduct == null)
            {
                throw new ArgumentNullException(nameof(basketProduct));
            }
            _appDBContext.Remove(basketProduct);
            _appDBContext.SaveChanges();
            //Basket basket = _appDBContext.
        }

        public Basket GetBasketById(int basketId)
        {
            return _appDBContext.Baskets.FirstOrDefault(p => p.Id == basketId);
        }
    }
}
