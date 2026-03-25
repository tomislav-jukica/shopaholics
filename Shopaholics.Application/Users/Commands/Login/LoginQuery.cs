using MediatR;
using Shopaholics.Application.Common;

namespace Shopaholics.Application.Users.Commands.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<Result<string>>;
}
