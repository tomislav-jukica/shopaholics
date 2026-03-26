using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Domain.Products;

namespace Shopaholics.Application.Products.Commands.GetFavouriteProductsQuery
{
    public record GetFavouriteProductsQuery(string UserId) : IRequest<Result<List<Product>>>;
}
