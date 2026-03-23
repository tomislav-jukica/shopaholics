using MediatR;
using Microsoft.AspNetCore.Identity;
using Shopaholics.Application.Common;
using Shopaholics.Application.Users.DTOs;
using Shopaholics.Domain.Users;

namespace Shopaholics.Application.Users.Commands.GetUser
{
    public class GetUserCommandHandler(UserManager<User> userManager) : IRequestHandler<GetUserCommand, Result<UserDto>>
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<Result<UserDto>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null) return Result<UserDto>.Failure("User not found.");

                UserDto userDto = new(user.Id, user.Email ?? throw new NullReferenceException("User e-mail is null!"));

                return Result<UserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                return Result<UserDto>.Failure(ex.Message);
            }
        }
    }
}
