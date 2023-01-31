using ISCardsWeb.Shared.Models;
using ISCardsWeb.Shared.Requests;
using ISCardsWeb.Shared.Responses;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ISCardsWeb.Client.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("api/Authentication/Login", request);

            var responseBody = await response.Content.ReadAsStringAsync();

            var s = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

            return s;
        }

        public async Task<RegisterResponse?> RegisterUserAsync(RegisterRequest request)
        {

                var response = await httpClient.PostAsJsonAsync("api/Authentication/Register", request);

                var responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<RegisterResponse>(responseBody);
  

        }

        public async Task<LoginResponse?> UpdateTokenAsync(string refreshToken)
        {
            var response = await httpClient.PostAsJsonAsync("api/Authentication/UpdateToken", refreshToken);

            var responseBody = await response.Content.ReadAsStringAsync();

           return JsonConvert.DeserializeObject<LoginResponse>(responseBody);

        }

        public async Task<User?> GetUserByAccessTokenAsync(string token)
        {
            var response = await httpClient.PostAsJsonAsync("api/Authentication/GetUserByAccessToken", token);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<User>(responseBody);

            return null;
        }

        public async Task SendConfirmationCodeAsync(string email) =>
            await httpClient.PostAsJsonAsync("api/Authentication/SendConfirmationCode", email);

        public async Task LogoutAsync(string token) 
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await httpClient.DeleteAsync("api/Authentication/Logout");
        }

       
    }
}
