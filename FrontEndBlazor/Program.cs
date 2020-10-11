using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FrontEndBlazor.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace FrontEndBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") });
            builder.Services.AddScoped<ProductHttpService>();
            builder.Services.AddScoped<BasketHttpService>();
            builder.Services.AddScoped<AuthenticationHttpService>();
            builder.Services.AddBlazoredLocalStorage();
            //builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore(options => { });
            
            //builder.Services.AddScoped<AuthenticationStateProvider>( p => p.GetService<CustomAuthStateProvider>());
            await builder.Build().RunAsync();
        }
    }
}
