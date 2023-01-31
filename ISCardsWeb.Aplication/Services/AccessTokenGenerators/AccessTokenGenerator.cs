using ISCardsWeb.Aplication.Services.TokenGenerators;
using ISCardsWeb.Shared.Configurations;
using ISCardsWeb.Shared.Models;
using System.Security.Claims;

namespace ISCardsWeb.Aplication.Services.AccessTokenGenerators
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly AuthenticationConfiguration configuration;
        private readonly ITokenGenerator tokenGenarator;

        public AccessTokenGenerator(AuthenticationConfiguration configuration,
            ITokenGenerator tokenGenarator)
        {
            this.configuration=configuration;
            this.tokenGenarator=tokenGenarator;
        }

        public string GenerateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            return tokenGenarator.GenerateToken(
                configuration.AccessTokenSecretKey,
                configuration.Issuer,
                configuration.Audience,
                configuration.AccessTokenExpirationTime,
                claims);
        }
    }
}
