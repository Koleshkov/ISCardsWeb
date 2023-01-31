using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCardsWeb.Application.Commands.Authentication.Login
{
    public class LoginVallidator : AbstractValidator<LoginCommand>
    {
        public LoginVallidator()
        {
            RuleFor(request => request.Email)
                .EmailAddress()
                .NotNull();

            RuleFor(request => request.Password)
                .NotNull();
        }
    }
}
