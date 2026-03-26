using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopaholics.Application.Products.Commands.AddToFavouritesCommand;
using Shopaholics.Application.Products.Commands.GetFavouriteProductsQuery;
using Shopaholics.Application.Users.Commands.GetUser;

namespace Shopaholics.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{userEmail}")]
        public async Task<IActionResult> GetUserFavouriteProducts(string userEmail)
        {
            try
            {
                var user = await _mediator.Send(new GetUserQuery(userEmail));
                if (!user.IsSuccess) return BadRequest("Failed to find user.");

                var result = await _mediator.Send(new GetFavouriteProductsQuery(user.Value!.Id));

                if (!result.IsSuccess) return BadRequest(result.Errors);

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavourite(string userEmail, int productId)
        {
            try
            {
                var user = await _mediator.Send(new GetUserQuery(userEmail));
                if (!user.IsSuccess) return BadRequest("Failed to find user.");

                var result = await _mediator.Send(new ToggleFavouriteCommand(user.Value!.Id, productId));
                if (!result.IsSuccess) return BadRequest(result.Errors);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
