using ISCardsWeb.Shared.Responses;

namespace ISCardsWeb.Client.Services.UserServices
{
    public interface IUserService
    {
        Task<UserResponse?> GetUserByNameAsync(string name);
    }
}
