using MediatR;
using Shopaholics.Application.Common;

namespace Shopaholics.Application.Products.Commands.AddToFavouritesCommand
{
    public record ToggleFavouriteCommand(string UserId, int ProductId) : IRequest<Result>;
}
