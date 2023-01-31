using ISCardsWeb.Shared.Models;

namespace ISCardsWeb.Aplication.Services.AccessTokenGenerators
{
    public interface IAccessTokenGenerator
    {
        string GenerateToken(User user);
    }
}
