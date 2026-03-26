using Microsoft.AspNetCore.Identity;
using Shopaholics.Domain.Favourites;

namespace Shopaholics.Domain.Users
{
    public class User : IdentityUser
    {
        public List<Favourite> Favourites { get; set; } = [];
    }
}
