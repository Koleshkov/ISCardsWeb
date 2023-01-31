using ISCardsWeb.Application.Commands.Authentication.GetUserByAccessToken;
using ISCardsWeb.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ISCardsWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ISCardsWeb.Aplication.Commands.UserCommands.GetUserByName
{
    public class GetUserByNameCommandHandler : IRequestHandler<GetUserByNameCommand, UserResponse?>
    {
        private readonly UserManager<User> userManager;

        public GetUserByNameCommandHandler(UserManager<User> userManager)
        {
            this.userManager=userManager;
        }

        public async Task<UserResponse?> Handle(GetUserByNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.Users.FirstOrDefaultAsync((u => u.Email==request.UserName), cancellationToken);


                if (user!=null)
                {
                    return new UserResponse
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        Department = user.Department,
                        Email = user.Email,
                        Organization = user.Organization,
                        PhoneNumber = user.PhoneNumber,
                        Position = user.Position
                    };
                }
                
            }
            catch (Exception)
            {

                return null;
            }
            return null;
        }
    }
}
