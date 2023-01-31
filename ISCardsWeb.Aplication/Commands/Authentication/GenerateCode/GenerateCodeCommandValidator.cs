using FluentValidation;

namespace ISCardsWeb.Application.Commands.Authentication.GenerateCode
{
    public class GenerateCodeCommandValidator : AbstractValidator<GenerateCodeCommand>
    {
        public GenerateCodeCommandValidator()
        {
            RuleFor(request => request.Email)
                .EmailAddress()
                .NotNull();
        }
    }
}
