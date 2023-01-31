using MediatR;

namespace ISCardsWeb.Application.Commands.Authentication.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest
    {
        public string Email { get; set; } = "";
        public string Code { get; set; } = "";

    }
}
