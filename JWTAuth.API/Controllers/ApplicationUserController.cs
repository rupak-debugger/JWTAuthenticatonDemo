using JWTAuthenticatonDemo.Application.Features.ApplicationUser.Commands;
using JWTAuthenticatonDemo.Application.Features.ApplicationUser.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationUserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] LoginUserQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
