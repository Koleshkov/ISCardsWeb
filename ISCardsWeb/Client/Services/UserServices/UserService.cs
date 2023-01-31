using ISCardsWeb.Shared.Models;
using ISCardsWeb.Shared.Responses;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ISCardsWeb.Client.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient=httpClient;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<UserResponse?> GetUserByNameAsync(string name)
        {
            var response = await httpClient.PostAsJsonAsync("api/User/GetUserByName", name);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<UserResponse>(responseBody);

            return null;
        }
    }
}
