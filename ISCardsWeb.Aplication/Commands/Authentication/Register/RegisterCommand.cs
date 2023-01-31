using AutoMapper;
using MediatR;
using ISCardsWeb.Application.Common.Mappings;
using ISCardsWeb.Shared.Requests;
using ISCardsWeb.Shared.Responses;
using ISCardsWeb.Shared.Models;

namespace ISCardsWeb.Application.Commands.Authentication.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>, IMapWith<RegisterRequest>
    {

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string ConfirmPassword { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterRequest, RegisterCommand>();
            profile.CreateMap<User, RegisterResponse>();
        }
    }
}
