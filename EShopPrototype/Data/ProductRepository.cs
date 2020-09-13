using EShopPrototype.Data.Interfaces;
using SharedItems.Models;
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

        public async Task<List<Product>> GetPaginatedProductsList(int pageNumber, int itemsPerPage)
        {
            int skipItems = (pageNumber * itemsPerPage) - itemsPerPage;
            List<Product> productList = _appDBContext.Products.Skip(skipItems).Take(itemsPerPage).ToList();
            foreach (var product in productList)
            {
                product.Price = await _blockchainRepository.GetProductPrice(product.Id);
            }
            return _appDBContext.Products.Skip(skipItems).Take(itemsPerPage).ToList();
        }

        public void UpdateProduct(Product product)
        {
            _appDBContext.Update(product);
            _appDBContext.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _appDBContext.Remove(product);
            _appDBContext.SaveChanges();
        }
        public async Task<Product> GetProductById(int id)
        {
            Product product = _appDBContext.Products.FirstOrDefault(p => p.Id == id);
            List<PriceHistory> productPriceHistory = await  _blockchainRepository.GetProductPriceHistory(id);
            product.PriceHistoryList = productPriceHistory;
            product.Price = await _blockchainRepository.GetProductPrice(id);
            return product;
        }

        
    }
}
