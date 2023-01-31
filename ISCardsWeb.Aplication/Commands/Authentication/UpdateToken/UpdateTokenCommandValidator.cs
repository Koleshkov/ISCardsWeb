using FluentValidation;

namespace ISCardsWeb.Application.Commands.Authentication.UpdateToken
{
    public class UpdateTokenCommandValidator : AbstractValidator<UpdateTokenCommand>
    {
        public UpdateTokenCommandValidator()
        {
            RuleFor(request => request.RefreshToken)
                .NotNull();
        }
    }
}
