using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ISCardsWeb.Shared.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ISCardsWeb.Shared.Models;

namespace ISCardsWeb.Application.Commands.Authentication.GetUserByAccessToken
{
    public class GetUserByAccessTokenCommandHandler : IRequestHandler<GetUserByAccessTokenCommand, User?>
    {
        private readonly AuthenticationConfiguration configuration;
        private readonly UserManager<User> userManager;

        public GetUserByAccessTokenCommandHandler(IOptions<AuthenticationConfiguration> configuration, UserManager<User> userManager)
        {
            this.configuration=configuration.Value;
            this.userManager=userManager;
        }

        public async Task<User?> Handle(GetUserByAccessTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(configuration.AccessTokenSecretKey);



                TokenValidationParameters tokenValidationParameters = new()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = configuration.Issuer,
                    ValidAudience = configuration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principle = tokenHandler.ValidateToken(request.Token, tokenValidationParameters, out SecurityToken securityToken);




                if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst("Id")?.Value;

                    var user = await userManager.Users.FirstOrDefaultAsync((u => u.Id.ToString() == userId), cancellationToken);

                    if (user!=null) user.PasswordHash="";


                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }
    }
}
