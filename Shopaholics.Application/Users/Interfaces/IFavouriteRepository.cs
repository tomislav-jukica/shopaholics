using Shopaholics.Domain.Favourites;

namespace Shopaholics.Application.Users.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<Favourite?> GetByIdAsync(int id);
        Task<List<Favourite>> GetByUserIdAsync(string userId);
        Task<Favourite?> GetByProductId(int productId, string userId);
        Task AddToFavorites(Favourite favourite);
        Task RemoveFromFavourites(Favourite favourite);
        Task SaveChangesAsync();
    }
}
