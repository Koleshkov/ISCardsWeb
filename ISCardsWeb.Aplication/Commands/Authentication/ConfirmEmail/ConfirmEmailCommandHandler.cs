using MediatR;
using Microsoft.AspNetCore.Identity;
using ISCardsWeb.Application.Common.Exceptions;
using ISCardsWeb.Shared.Models;

namespace ISCardsWeb.Application.Commands.Authentication.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
    {
        private readonly UserManager<User> userManager;

        public ConfirmEmailCommandHandler(UserManager<User> userManager)
        {
            this.userManager=userManager;
        }

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new NotFoundException(request.Email);
            }

            if (user.EmailConfirmed)
            {
                throw new AlreadyExistException(request.Email);
            }

            var result = await userManager.ConfirmEmailAsync(user, request.Code);

            if (!result.Succeeded)
            {
                throw new Exception("Oups, something went wrong.");
            }

            return Unit.Value;
        }
    }
}
