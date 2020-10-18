using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using SharedItems.Models;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace FrontEndBlazor.Services
{
    public class ProductHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStore;

        public ProductHttpService(HttpClient httpClient, ILocalStorageService localStore)
        {
            _httpClient = httpClient;
            _localStore = localStore;
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

        public async void AddProduct(Product product)
        {
            string token = await _localStore.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage result = await _httpClient.PostAsJsonAsync<Product>("Product", product);
            result.EnsureSuccessStatusCode();
            string a = "";
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
