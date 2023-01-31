using FluentValidation;

namespace ISCardsWeb.Application.Commands.Authentication.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(request => request.Code)
                .NotNull();

            RuleFor(request => request.Email)
                .EmailAddress()
                .NotNull();
        }
    }
}
