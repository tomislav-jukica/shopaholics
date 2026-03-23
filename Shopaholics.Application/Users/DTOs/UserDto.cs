namespace Shopaholics.Application.Users.DTOs
{
    public class UserDto(string id, string email)
    {
        public string Id { get; set; } = id;
        public string Email { get; set; } = email;
    }
}
