using FluentValidation;


namespace ISCardsWeb.Aplication.Commands.UserCommands.GetUserByName
{
    public class GetUserByNameCommandValidation : AbstractValidator<GetUserByNameCommand>
    {
        public GetUserByNameCommandValidation()
        {
            RuleFor(request => request.UserName)
                .NotNull();
        }

    }
}
