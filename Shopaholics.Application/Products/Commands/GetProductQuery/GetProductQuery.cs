using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Domain.Products;

namespace Shopaholics.Application.Products.Commands.GetProductQuery
{
    public record GetProductQuery(int Id) : IRequest<Result<Product>>;
}
