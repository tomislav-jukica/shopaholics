using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopaholics.Application.Products.Commands.AddToFavouritesCommand;
using Shopaholics.Application.Products.Commands.GetFavouriteProductsQuery;

namespace Shopaholics.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserFavouriteProducts(string userId)
        {
            try
            {
                var result = await _mediator.Send(new GetFavouriteProductsQuery(userId));

                if (!result.IsSuccess) return BadRequest(result.Errors);

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavourite(string userId, int productId)
        {
            try
            {
                var result = await _mediator.Send(new ToggleFavouriteCommand(userId, productId));
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
