using SharedItems.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace FrontEndBlazor.Services
{
    public class AuthenticationHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStore;
        private readonly NavigationManager _navigationManager;

        public AuthenticationHttpService(HttpClient httpClient, ILocalStorageService localStore, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _localStore = localStore;
            _navigationManager = navigationManager;

        }

        public async void LogoutUser()
        {
            //await _localStore.RemoveItemAsync("userName");
            //await _localStore.RemoveItemAsync("authToken");
            await _localStore.ClearAsync();
            _navigationManager.NavigateTo("/");
        }

        public async void Login(User user)
        {
            try
            {
                var todoItemJson = new StringContent(JsonSerializer.Serialize(user),Encoding.UTF8,"application/json");
                //var token = await _httpClient.PostAsync("auth/login", todoItemJson);
                HttpResponseMessage result = await _httpClient.PostAsJsonAsync<User>("auth/login", user);
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    string bearerTokenValue = await result.Content.ReadAsStringAsync();
                    //string tokenStorageKey = "authToken";
                    await _localStore.SetItemAsync("authToken", bearerTokenValue);
                    await _localStore.SetItemAsync("userName", user.Username);
                }
                //HttpContent token = result.Content;
                //string converted = Encoding.UTF8.GetString(, 0, buffer.Length);
                string a = "";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
