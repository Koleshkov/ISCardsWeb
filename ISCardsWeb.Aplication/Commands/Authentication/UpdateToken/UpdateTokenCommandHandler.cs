using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ISCardsWeb.Shared.Models;
using ISCardsWeb.Shared.Responses;
using ISCardsWeb.Aplication.Services;
using ISCardsWeb.Aplication.Services.AccessTokenGenerators;
using ISCardsWeb.Aplication.Services.RefreshTokenGenerators;
using ISCardsWeb.Aplication.Services.RefreshTokenValidators;

namespace ISCardsWeb.Application.Commands.Authentication.UpdateToken
{
    public class UpdateTokenCommandHandler : IRequestHandler<UpdateTokenCommand, LoginResponse>
    {
        private readonly IAccessTokenGenerator accessTokenGenerator;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;
        private readonly IRefreshTokenValidator refreshTokenValidator;
        private readonly IAppDbContext context;
        private readonly UserManager<User> userManager;

        public UpdateTokenCommandHandler(IAccessTokenGenerator accessTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenValidator refreshTokenValidator,
            IAppDbContext context,
            UserManager<User> userManager)
        {
            this.accessTokenGenerator=accessTokenGenerator;
            this.refreshTokenGenerator=refreshTokenGenerator;
            this.refreshTokenValidator=refreshTokenValidator;
            this.context=context;
            this.userManager=userManager;
        }

        public async Task<LoginResponse> Handle(UpdateTokenCommand request, CancellationToken cancellationToken)
        {
            var isValidRefreshToken = refreshTokenValidator.Validate(request.RefreshToken);

            if (!isValidRefreshToken)
            {
                throw new Exception("Invalid refresh token");
            }

            var refreshTokenTemp =
                await context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token==request.RefreshToken, cancellationToken);

            if (refreshTokenTemp==null)
            {
                throw new Exception("Invalid refresh token");
            }

            context.RefreshTokens.Remove(refreshTokenTemp);
            await context.SaveChangesAsync(cancellationToken);

            var user = await userManager.FindByIdAsync(refreshTokenTemp.UserId.ToString());

            if (user==null)
            {
                throw new Exception("User not found.");
            }

            var accessToken = accessTokenGenerator.GenerateToken(user);
            var refreshToken = refreshTokenGenerator.GenerateToken();

            refreshTokenTemp = new()
            {
                Token = refreshToken,
                UserId = user.Id
            };

            await context.RefreshTokens.AddAsync(refreshTokenTemp, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new LoginResponse
            {
                AccessToken=accessToken,
                RefreshToken=refreshToken
            };
        }
    }
}
