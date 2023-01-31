using MediatR;
using Microsoft.AspNetCore.Identity;
using ISCardsWeb.Aplication.Services;
using ISCardsWeb.Aplication.Services.AccessTokenGenerators;
using ISCardsWeb.Aplication.Services.RefreshTokenGenerators;
using ISCardsWeb.Shared.Responses;
using ISCardsWeb.Shared.Models;

namespace ISCardsWeb.Application.Commands.Authentication.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly IAccessTokenGenerator accessTokenGenerator;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;
        private readonly IAppDbContext appDbContext;

        public LoginCommandHandler(UserManager<User> userManager,
            IAccessTokenGenerator accessTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IAppDbContext appDbContext)
        {
            this.userManager=userManager;
            this.accessTokenGenerator=accessTokenGenerator;
            this.refreshTokenGenerator=refreshTokenGenerator;
            this.appDbContext=appDbContext;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user==null)
            {
                throw new Exception($"Адрес {request.Email} не зарегистриван.");
            }
            if (!user.EmailConfirmed)
            {
                throw new Exception($"Адрес {request.Email} не подтверден. " +
                    $"Для завершения регистрации перейдите по ссылке отправленной на " +
                    $"указанный адрес.");
            }

            bool isCorrectPassword = await userManager.CheckPasswordAsync(user, request.Password);

            if (!isCorrectPassword)
            {
                throw new Exception($"Не верный пароль.");
            }



            var accessToken = accessTokenGenerator.GenerateToken(user);
            var refreshToken = refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenTemp = new()
            {
                Token = refreshToken,
                UserId = user.Id
            };

            await appDbContext.RefreshTokens.AddAsync(refreshTokenTemp, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return new LoginResponse
            { 
                Email = user.Email,
                UserName = user.UserName,
                AccessToken=accessToken, 
                RefreshToken=refreshToken 
            };
        }
    }
}
