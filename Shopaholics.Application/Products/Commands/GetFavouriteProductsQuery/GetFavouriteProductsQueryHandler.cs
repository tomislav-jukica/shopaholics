using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Application.Products.Interfaces;
using Shopaholics.Application.Users.Interfaces;
using Shopaholics.Domain.Products;

namespace Shopaholics.Application.Products.Commands.GetFavouriteProductsQuery
{
    public class GetFavouriteProductsQueryHandler(IFavouriteRepository favouriteRepository, IProductService productService) : IRequestHandler<GetFavouriteProductsQuery, Result<List<Product>>>
    {
        private readonly IFavouriteRepository _favouriteRepository = favouriteRepository;
        private readonly IProductService _productService = productService;

        public async Task<Result<List<Product>>> Handle(GetFavouriteProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsAsync();
            if (products == null) return Result<List<Product>>.Failure("Failed to fetch products.");

            var favouriteProducts = await _favouriteRepository.GetByUserIdAsync(request.UserId);

            var result = products.Where(p => favouriteProducts.Any(f => f.ProductId == p.Id)).ToList();

            return Result<List<Product>>.Success(result);
        }
    }
}
