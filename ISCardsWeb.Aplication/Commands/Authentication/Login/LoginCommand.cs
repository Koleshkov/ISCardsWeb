using AutoMapper;
using MediatR;
using ISCardsWeb.Application.Common.Mappings;
using ISCardsWeb.Shared.Requests;
using ISCardsWeb.Shared.Responses;

namespace ISCardsWeb.Application.Commands.Authentication.Login
{
    public class LoginCommand : IRequest<LoginResponse>, IMapWith<LoginRequest>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginRequest, LoginCommand>();
        }
    }
}
