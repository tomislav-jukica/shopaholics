using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Application.Products.Interfaces;
using Shopaholics.Domain.Products;

namespace Shopaholics.Application.Products.Commands.GetProductQuery
{
    public class GetProductQueryHandler(IProductService productService) : IRequestHandler<GetProductQuery, Result<Product>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductAsync(request.Id);
            if (product == null)
            {
                return Result<Product>.Failure("Failed to find product.");
            }

            return Result<Product>.Success(product);
        }
    }
}
