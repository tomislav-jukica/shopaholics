using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Application.Products.Interfaces;
using Shopaholics.Domain.Products;

namespace Shopaholics.Application.Products.Commands.GetProductsQuery
{
    public class GetProductsQueryHandler(IProductService productService) : IRequestHandler<GetProductsQuery, Result<List<Product>>>
    {
        private readonly IProductService _productService = productService;

        public async Task<Result<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsAsync();

            return Result<List<Product>>.Success(products);
        }
    }
}
