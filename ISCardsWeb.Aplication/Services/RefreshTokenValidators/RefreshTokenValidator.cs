using Microsoft.IdentityModel.Tokens;
using ISCardsWeb.Shared.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ISCardsWeb.Aplication.Services.RefreshTokenValidators;

namespace ISCardsWeb.Aplication.Services.RefreshTokenValidators
{ 
    public class RefreshTokenValidator : IRefreshTokenValidator
    {
        private readonly AuthenticationConfiguration _configuration;

        public RefreshTokenValidator(AuthenticationConfiguration configuration)
        {
            _configuration=configuration;
        }

        public bool Validate(string refreshToken)
        {
            TokenValidationParameters validationParameters = new()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.RefreshTokenSecretKey)),
                ValidIssuer = _configuration.Issuer,
                ValidAudience = _configuration.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
