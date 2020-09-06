using EShopPrototype.Data.Interfaces;
using EShopPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopPrototype.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _appDBContext;
        private readonly BlockchainRepository _blockchainRepository;

        public ProductRepository(AppDBContext appDBContext, BlockchainRepository blockchainRepository)
        {
            _appDBContext = appDBContext;
            _blockchainRepository = blockchainRepository;
        }

        public async Task CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _appDBContext.Products.Add(product);
            _appDBContext.SaveChanges();

            /* TODO - error handle it */
            bool successful = await _blockchainRepository.SetProductPrice(product.Id, product.Price);
        }

        public Product GetProductById(int id)
        {
            _blockchainRepository.GetProductPriceHistory(id);
            return _appDBContext.Products.FirstOrDefault(p => p.Id == id);
        }

        
    }
}
