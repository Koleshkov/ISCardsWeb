using FluentValidation;

namespace ISCardsWeb.Application.Commands.Authentication.GetUserByAccessToken
{
    public class GetUserByAccessTokenCommandValidator:AbstractValidator<GetUserByAccessTokenCommand>
    {
        public GetUserByAccessTokenCommandValidator()
        {
            RuleFor(request => request.Token)
                .NotNull();
        }
    }
}
