using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using SharedItems.Models;

namespace FrontEndBlazor.Services
{
    public class ProductHttpService
    {
        private readonly HttpClient _httpClient;

        public ProductHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetPaginatedProductsList(int page, int itemsPerPage)
        {
            try
            {
                string requestString = String.Format("Product/PageNumber/{0}/ItemsPerPage/{1}", page, itemsPerPage);
                List<Product> productList = await _httpClient.GetFromJsonAsync<List<Product>>(requestString);
                return productList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Product> GetProductById(UInt64 id)
        {
            try
            {
                string requestString = String.Format("Product/{0}", id);
                Product product = await _httpClient.GetFromJsonAsync<Product>(requestString);
                return product;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
