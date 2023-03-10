using ISCardsWeb.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace ISCardsWeb.Application.Commands.Authentication.GenerateCode
{
    public class GenerateCodeCommandHandler : IRequestHandler<GenerateCodeCommand, string>
    {
        private readonly UserManager<User> userManager;

        public GenerateCodeCommandHandler(UserManager<User> userManager)
        {
            this.userManager=userManager;
        }

        public async Task<string> Handle(GenerateCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user==null)
            {
                throw new Exception($"\"{request.Email}\" not found");
            }
            if (user.EmailConfirmed)
            {
                throw new Exception($"\"{request.Email}\" already confirmed");
            }

            string code = await userManager.GenerateEmailConfirmationTokenAsync(user);

            return code;
        }
    }
}
