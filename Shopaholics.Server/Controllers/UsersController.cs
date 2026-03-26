using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopaholics.Application.Users.Commands.CreateUser;
using Shopaholics.Application.Users.Commands.GetUser;
using Shopaholics.Application.Users.Commands.Login;
using Shopaholics.Application.Users.DTOs;

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

                if (result.IsSuccess) return Ok();

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _mediator.Send(new LoginQuery(dto.Email, dto.Password));

            if (!result.IsSuccess) return Unauthorized(result.Errors);

            return Ok(new { token = result.Value });
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] GetUserQuery command)
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
