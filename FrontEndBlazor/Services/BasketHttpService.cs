using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using SharedItems.Models;

namespace FrontEndBlazor.Services
{
    public class BasketHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStore;

        public BasketHttpService(HttpClient httpClient, ILocalStorageService localStore)
        {
            _httpClient = httpClient;
            _localStore = localStore;
        }

        public async void AddProductToBasket(Product product)
        {
            Basket basket = new Basket()
            {
                ProductId = product.Id,
                ProductQuanity = 1
            };
            string token = await _localStore.GetItemAsync<string>("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage result = await _httpClient.PostAsJsonAsync<Basket>("Basket/AddProduct", basket);
        }

    }
}
