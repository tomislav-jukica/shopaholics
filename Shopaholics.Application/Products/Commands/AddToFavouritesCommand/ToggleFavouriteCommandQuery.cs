using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Application.Users.Interfaces;
using Shopaholics.Domain.Favourites;

namespace Shopaholics.Application.Products.Commands.AddToFavouritesCommand
{
    public class ToggleFavouriteCommandQuery(IFavouriteRepository favouriteRepository) : IRequestHandler<ToggleFavouriteCommand, Result>
    {
        private readonly IFavouriteRepository _favouriteRepository = favouriteRepository;

        public async Task<Result> Handle(ToggleFavouriteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var favourite = await _favouriteRepository.GetByProductId(request.ProductId, request.UserId);
                if (favourite == null)
                {
                    await _favouriteRepository.AddToFavorites(new Favourite()
                    {
                        UserId = request.UserId,
                        ProductId = request.ProductId,
                    });
                }
                else
                {
                    await _favouriteRepository.RemoveFromFavourites(favourite);
                }

                await _favouriteRepository.SaveChangesAsync();
                
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.ToString());
            }
        }
    }
}
