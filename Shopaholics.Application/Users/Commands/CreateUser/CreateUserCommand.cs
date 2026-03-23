using MediatR;
using Shopaholics.Application.Common;

namespace Shopaholics.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(string Email, string Password) : IRequest<Result>;
}
