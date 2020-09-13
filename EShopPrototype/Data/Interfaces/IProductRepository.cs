using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopPrototype.Data.Interfaces
{
    public interface IProductRepository
    {
        public Task CreateProduct(Product product);
        public Task<Product> GetProductById(int id);
        public void DeleteProduct(Product product);
        public List<Product> GetPaginatedProductsList(int pageNumber, int itemsPerPage);

        public void UpdateProduct(Product product);
    }
}
