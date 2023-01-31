using ISCardsWeb.Shared.Models;
using ISCardsWeb.Shared.Requests;
using ISCardsWeb.Shared.Responses;

namespace ISCardsWeb.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<RegisterResponse?> RegisterUserAsync(RegisterRequest request);
        Task<LoginResponse?> UpdateTokenAsync(string refreshToken);
        Task SendConfirmationCodeAsync(string email);
        Task<User?> GetUserByAccessTokenAsync(string token);
        Task LogoutAsync(string token);
    }
}
