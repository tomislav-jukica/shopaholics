using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Domain.Products;

namespace Shopaholics.Application.Products.Commands.GetProductsQuery
{
    public record GetProductsQuery() : IRequest<Result<List<Product>>>;
}
