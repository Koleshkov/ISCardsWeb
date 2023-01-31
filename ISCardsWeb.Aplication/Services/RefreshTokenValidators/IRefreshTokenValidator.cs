namespace ISCardsWeb.Aplication.Services.RefreshTokenValidators
{
    public interface IRefreshTokenValidator
    {
        bool Validate(string refreshToken);
    }
}
