using ISCardsWeb.Shared.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISCardsWeb.Aplication.Commands.UserCommands.GetUserByName
{
    public class GetUserByNameCommand : IRequest<UserResponse?>
    {
        public string UserName { get; set; } = "";
    }
}
