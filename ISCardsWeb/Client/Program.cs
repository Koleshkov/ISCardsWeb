using Blazored.LocalStorage;
using ISCardsWeb.Client.Data;
using ISCardsWeb.Client.PageModels;
using ISCardsWeb.Client.Services.Authentication;
using ISCardsWeb.Client.Services.UserServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ISCardsWeb.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            //Add Services
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(
                "ISCards", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<IUserService, UserService>(
                "ISCards", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddSingleton<HttpClient>();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddBlazoredLocalStorage();

            //Add PageModels
            builder.Services.AddTransient<LoginPageModel>();

            builder.Services.AddTransient<RegisterPageModel>();

            await builder.Build().RunAsync();
        }
    }
}