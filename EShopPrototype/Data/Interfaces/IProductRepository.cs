using EShopPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopPrototype.Data.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);
        public Product GetProductById(int id);
    }
}
