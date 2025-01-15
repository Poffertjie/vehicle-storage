using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AdminPortal.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Shared;

namespace AdminPortal
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpService _httpService;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpService httpService)
        {
            _localStorageService = localStorageService;
            _httpService = httpService;
        }

        public async Task ClearTokenAsync()
        {
            _httpService.RemoveAuthToken();
            await _localStorageService.RemoveItemAsync(StringConstants.Authorization);
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorageService.SetItemAsync(StringConstants.Authorization, token);
            NotifyAuthenticationStateChanged(Task.FromResult(await AuthState(token)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await GetTokenAsync();

                if (string.IsNullOrWhiteSpace(token))
                    return await AuthState();

                if (await ValidateTokenExpiration(token))
                    return await AuthState(token);

                await ClearTokenAsync();
                return await AuthState();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return await AuthState();
            }
        }

        public async Task<string> GetTokenAsync()
        {
            var result = await _localStorageService.GetItemAsync<string>(StringConstants.Authorization);
            return !string.IsNullOrWhiteSpace(result) ? result : "";
        }

        private Task<AuthenticationState> AuthState(string? token = null)
        {
            if (token == null)
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

                _httpService.SetAuthToken(token);
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(GetClaimsFromJWT(token), "jwt"))));
        }

        private Task<bool> ValidateTokenExpiration(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(jwt);

            if (jwtToken.ValidTo < DateTime.UtcNow.AddMinutes(1))
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }

        private IEnumerable<Claim> GetClaimsFromJWT(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(jwt);
            return jwtToken.Claims;
        }
    }
}
