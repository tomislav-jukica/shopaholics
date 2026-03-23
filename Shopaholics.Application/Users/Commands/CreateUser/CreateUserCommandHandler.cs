using MediatR;
using Microsoft.AspNetCore.Identity;
using Shopaholics.Application.Common;
using Shopaholics.Domain.Users;

namespace Shopaholics.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(UserManager<User> userManager) : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded) return Result.Success();

                return Result.Failure(result.Errors.Select(x => x.Description).ToArray());
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.ToString());
            }
        }
    }
}
