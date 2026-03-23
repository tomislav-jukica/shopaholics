using Microsoft.AspNetCore.Identity;
using Shopaholics.Application.Users.Interfaces;
using Shopaholics.Domain.Users;

namespace Shopaholics.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
