using MediatR;
using Shopaholics.Application.Common;
using Shopaholics.Application.Users.DTOs;

namespace Shopaholics.Application.Users.Commands.GetUser
{
    public record GetUserQuery(string Email) : IRequest<Result<UserDto>>;
}
