using JWTAuthenticatonDemo.Application.Features.Authentication.Commands;
using JWTAuthenticatonDemo.Application.Features.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
