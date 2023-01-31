using ISCardsWeb.Aplication.Services.RefreshTokenGenerators;
using ISCardsWeb.Aplication.Services.TokenGenerators;
using ISCardsWeb.Shared.Configurations;

namespace ISCardsWeb.Aplication.Services.RefreshTokenGenerators
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly AuthenticationConfiguration configuration;
        private readonly ITokenGenerator tokenGenerator;

        public RefreshTokenGenerator(AuthenticationConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            this.configuration=configuration;
            this.tokenGenerator=tokenGenerator;
        }

        public string GenerateToken()
        {
            return tokenGenerator.GenerateToken(
                configuration.RefreshTokenSecretKey,
                configuration.Issuer,
                configuration.Audience,
                configuration.RefreshTokenExpirationTime);
        }
    }
}
