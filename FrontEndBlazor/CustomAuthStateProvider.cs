using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStore;
    public CustomAuthStateProvider(ILocalStorageService localStore)
    {
        _localStore = localStore;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (await _localStore.ContainKeyAsync("userName"))
        {
            string token = await _localStore.GetItemAsync<string>("authToken");
            string userName = await _localStore.GetItemAsync<string>("userName");
            var claims = new[]
            {
                new Claim("UserName", userName),
                new Claim("Token", token)
            };

            var identity = new ClaimsIdentity(claims, "BearerToken");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        return new AuthenticationState(new ClaimsPrincipal());

    }
}