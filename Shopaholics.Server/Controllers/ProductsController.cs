using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopaholics.Application.Products.Commands.GetProductsQuery;

namespace Shopaholics.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var result = await _mediator.Send(new GetProductsQuery());

                if (!result.IsSuccess) return BadRequest(result.Errors);

                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
