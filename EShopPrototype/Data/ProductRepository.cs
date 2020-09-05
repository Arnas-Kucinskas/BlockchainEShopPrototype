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

        public ProductRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public void CreateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _appDBContext.Products.Add(product);
            _appDBContext.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _appDBContext.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
