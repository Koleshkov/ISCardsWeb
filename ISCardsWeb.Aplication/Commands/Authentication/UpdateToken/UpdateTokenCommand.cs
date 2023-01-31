using MediatR;
using ISCardsWeb.Shared.Responses;

namespace ISCardsWeb.Application.Commands.Authentication.UpdateToken
{
    public class UpdateTokenCommand : IRequest<LoginResponse>
    {
        public string RefreshToken { get; set; } = "";
    }
}
