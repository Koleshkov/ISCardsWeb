using FluentValidation;

namespace ISCardsWeb.Application.Commands.Authentication.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(command => command.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(command => command.Password)
                .NotEmpty();

            RuleFor(command => command.ConfirmPassword)
                .NotEmpty()
                .Equal(command => command.Password);
        }
    }
}
