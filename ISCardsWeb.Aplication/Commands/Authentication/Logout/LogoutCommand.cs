using MediatR;

namespace ISCardsWeb.Application.Commands.Authentication.Logout
{
    public class LogoutCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
