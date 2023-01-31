using MediatR;

namespace ISCardsWeb.Application.Commands.Authentication.GenerateCode
{
    public class GenerateCodeCommand : IRequest<string>
    {
        public string Email { get; set; } = "";
    }
}
