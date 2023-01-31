using Blazored.LocalStorage;
using ISCardsWeb.Client.Services.Authentication;
using ISCardsWeb.Shared.Models;
using ISCardsWeb.Shared.Requests;
using ISCardsWeb.Shared.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ISCardsWeb.Client.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly IAuthenticationService authenticationService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage, IAuthenticationService authenticationService)
        {
            this.localStorage=localStorage;
            this.authenticationService=authenticationService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;

            var accessToken = await localStorage.GetItemAsync<string>("accessToken");
            var refreshToken = await localStorage.GetItemAsync<string>("refreshToken");

            if (accessToken != null && accessToken != string.Empty
                && refreshToken != null && refreshToken != string.Empty)
            {
                var user = await authenticationService.GetUserByAccessTokenAsync(accessToken);

                if (user!=null)
                {
                    identity = GetClaimsIdentity(user);
                }
                else
                {
                    var response = await authenticationService
                        .UpdateTokenAsync(refreshToken);

                    if (response!=null)
                    {
                        user = await authenticationService.GetUserByAccessTokenAsync(response.AccessToken);
                        if (user!=null)
                        {
                            await SaveTokens(response.AccessToken, response.RefreshToken);
                            identity = GetClaimsIdentity(user);
                        }
                        else identity = new ClaimsIdentity();
                    }
                    else identity = new ClaimsIdentity();
                }
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task<LoginResponse> MarkUserAsAuthenticatedAsync(LoginRequest request)
        {
            ClaimsIdentity identity;

            LoginResponse response = await authenticationService.LoginAsync(request)??
                new LoginResponse { Errors = new List<string> { "Нет связи с сервером" } };
            if (response.Errors==null)
            {
                var user = await authenticationService.GetUserByAccessTokenAsync(response.AccessToken);

                if (user!=null)
                {
                    await SaveTokens(response.AccessToken, response.RefreshToken);

                    identity = GetClaimsIdentity(user);

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
                }
            }

            return await Task.FromResult(response);
        }

        public async Task MarkUserAsLoggedOutAsync()
        {
            await GetAuthenticationStateAsync();

            var token = await localStorage.GetItemAsync<string>("accessToken");

            await authenticationService.LogoutAsync(token);

            await localStorage.RemoveItemAsync("accessToken");
            await localStorage.RemoveItemAsync("refreshToken");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private static ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim("id", user.Id.ToString()),
                                    new Claim(ClaimTypes.Email, user.Email),
                                    new Claim(ClaimTypes.Name, user.UserName)
                                }, "apiauth_type");

            return claimsIdentity;
        }

        private async Task SaveTokens(string accessToken, string refreshToken)
        {
            await localStorage.SetItemAsync("accessToken", accessToken);
            await localStorage.SetItemAsync("refreshToken", refreshToken);
        }
    }
}
