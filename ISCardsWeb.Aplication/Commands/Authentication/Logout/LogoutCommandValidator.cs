using FluentValidation;

namespace ISCardsWeb.Application.Commands.Authentication.Logout
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(request => request.UserId)
                .NotNull();
        }
    }
}
