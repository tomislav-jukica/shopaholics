using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopaholics.Application.Users.Commands.CreateUser;
using Shopaholics.Application.Users.Commands.GetUser;

namespace Shopaholics.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccess) return Ok("User created successfully");

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] GetUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result.IsSuccess) return Ok(result.Value);

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
