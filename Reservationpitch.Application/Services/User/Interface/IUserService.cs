using Microsoft.AspNetCore.Identity;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.UserDtos.Request;
using Reservationpitch.Application.DTOs.UserDtos.Response;
using Reservationpitch.Domain.Entities;

namespace Reservationpitch.Application.Services.User.Interface
{
    public interface IUserService
    {
        Task<Result<LoginResponseDto>> Login(LoginRequestDto request);
        Task<Result<UserResponseDto>> Register(RegisterRequestDto request);
        Task<Result<bool>> AddUserToRole(UserRoleRequestDto request);
        Task<Result<string>> GenerateUserEmailConfirmationTokenAsync(SystemUser user);
        Task<Result<SystemUser>> FindUserByIdAsync(string userId);
        Task<Result<IdentityResult>> ConfirmUserEmailAsync(SystemUser user, string token);
        Task<Result<SystemUser>> FindUserByUsernameAsync(string username);
        Task<Result<IEnumerable<string>>> GetUserRolesAsync(SystemUser user);
        Task<Result<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);

    }
}
