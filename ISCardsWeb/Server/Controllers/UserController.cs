using AutoMapper;
using ISCardsWeb.Aplication.Commands.UserCommands.GetUserByName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISCardsWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public UserController(IMapper mapper, IMediator mediator)
        {
            this.mapper=mapper;
            this.mediator=mediator;
        }

        [HttpPost("GetUserByName")]
        public async Task<IActionResult> GetUserByName([FromBody] string userName)
        {
            GetUserByNameCommand command = new()
            {
                UserName=userName
            };

            var response = await mediator.Send(command);

            if (response==null) return BadRequest("Bad token");

            return Ok(response);
        }
    }
}
