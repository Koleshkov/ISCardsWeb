using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NETCore.MailKit.Core;
using ISCardsWeb.Shared.Requests;
using ISCardsWeb.Application.Commands.Authentication.Register;
using ISCardsWeb.Application.Commands.Authentication.GenerateCode;
using ISCardsWeb.Application.Commands.Authentication.ConfirmEmail;
using ISCardsWeb.Application.Commands.Authentication.Login;
using ISCardsWeb.Application.Commands.Authentication.GetUserByAccessToken;
using ISCardsWeb.Application.Commands.Authentication.UpdateToken;
using ISCardsWeb.Application.Commands.Authentication.Logout;

namespace ISCardsWeb.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IMediator mediator;
        private readonly IEmailService emailService;
        private readonly IMapper mapper;

        public AuthenticationController(IMediator mediator, IEmailService emailService,
            IMapper mapper)
        {
            this.mediator=mediator;
            this.emailService=emailService;
            this.mapper=mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = mapper.Map<RegisterCommand>(request);

            var response = await mediator.Send(command);

            await SendConfirmationCode(command.Email);

            return Ok(response);
        }

        [HttpPost("SendConfirmationCode")]
        private async Task<IActionResult> SendConfirmationCode(string email)
        {
            var code = await mediator.Send(new GenerateCodeCommand { Email=email });

            var callbackUrl = Url.Action("ConfirmEmail", "Authentication", new { email, code },
                        protocol: HttpContext.Request.Scheme);

            await emailService.SendAsync(email, "Подтверждение регистрации",
            $"<H1>Подтвердите регистрацию, перейдя по <a href='{callbackUrl}'>ссылке</a>.</H1>",
            true);

            return Ok();
        }

        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var command = new ConfirmEmailCommand()
            {
                Email=email,
                Code = code
            };

            await mediator.Send(command);

            ViewData["Email"] = email;

            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = mapper.Map<LoginCommand>(request);

            var response = await mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("GetUserByAccessToken")]
        public async Task<IActionResult> GetUserByAccessToken([FromBody] string accessToken)
        {
            GetUserByAccessTokenCommand command = new()
            {
                Token = accessToken
            };

            var response = await mediator.Send(command);

            if (response==null) return BadRequest("Bad token");

            return Ok(response);
        }

        [HttpPost("UpdateToken")]
        public async Task<IActionResult> UpdateToken([FromBody] string refreshToken)
        {
            var command = new UpdateTokenCommand() { RefreshToken= refreshToken };

            var response = await mediator.Send(command);

            return Ok(response);
        }

        
        [HttpDelete("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");

            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            LogoutCommand logoutRequest = new()
            {
                UserId = userId
            };

            await mediator.Send(logoutRequest);

            return NoContent();
        }
    }
}
