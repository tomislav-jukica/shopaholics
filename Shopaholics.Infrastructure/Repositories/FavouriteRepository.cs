using Microsoft.EntityFrameworkCore;
using Shopaholics.Application.Users.Interfaces;
using Shopaholics.Domain.Favourites;
using Shopaholics.Infrastructure.Persistance;

namespace Shopaholics.Infrastructure.Repositories
{
    public class FavouriteRepository(UserDbContext dbContext) : IFavouriteRepository
    {
        private readonly UserDbContext _dbContext = dbContext;

        public async Task AddToFavorites(Favourite favourite)
        {
            await _dbContext.Favourites.AddAsync(favourite);
        }

        public async Task<Favourite?> GetByIdAsync(int id)
        {
            var favourite = await _dbContext.Favourites.FirstOrDefaultAsync(x => x.Id == id);
            return favourite;
        }

        public async Task<Favourite?> GetByProductId(int productId, string userId)
        {
            var favourite = await _dbContext.Favourites.FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);
            return favourite;
        }

        public async Task<List<Favourite>> GetByUserIdAsync(string userId)
        {
            var favourites = await _dbContext.Favourites.Where(x => x.UserId == userId).ToListAsync();
            return favourites;
        }

        public async Task RemoveFromFavourites(Favourite favourite)
        {
            _dbContext.Favourites.Remove(favourite);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
