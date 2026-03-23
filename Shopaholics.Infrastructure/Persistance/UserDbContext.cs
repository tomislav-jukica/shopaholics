using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopaholics.Domain.Users;

namespace Shopaholics.Infrastructure.Persistance
{
    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
    }
}
