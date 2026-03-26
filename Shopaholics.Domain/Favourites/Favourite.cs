using Shopaholics.Domain.Users;

namespace Shopaholics.Domain.Favourites
{
    public class Favourite
    {
        public int Id { get; set; }

        public required string UserId { get; set; }
        public User User { get; set; } = null!;

        public int ProductId { get; set; }
    }
}
