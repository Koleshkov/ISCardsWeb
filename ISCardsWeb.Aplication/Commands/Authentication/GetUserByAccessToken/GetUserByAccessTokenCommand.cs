using MediatR;
using ISCardsWeb.Shared.Responses;
using ISCardsWeb.Shared.Models;

namespace ISCardsWeb.Application.Commands.Authentication.GetUserByAccessToken
{
    public class GetUserByAccessTokenCommand : IRequest<User?>
    {
        public string Token { get; set; } = "";
    }
}
