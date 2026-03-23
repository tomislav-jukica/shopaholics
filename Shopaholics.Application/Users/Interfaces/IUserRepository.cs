using Shopaholics.Domain.Users;

namespace Shopaholics.Application.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> CreateAsync(User user, string password);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(User user);
    }
}
