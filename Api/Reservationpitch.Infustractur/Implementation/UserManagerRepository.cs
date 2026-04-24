using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reservationpitch.Domain.Common.Enums;
using Reservationpitch.Domain.Common.Exceptions;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Database;
using System.Security.Claims;

namespace Reservationpitch.Infustracture.Implementation
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SystemUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManagerRepository(ApplicationDbContext context, UserManager<SystemUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(SystemUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result;
        }

        public async Task<bool> AssignRoleToUser(string email, string roleName)
        {
            var result = await _context.systemUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (result != null)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await AddUserToRoleAsync(result, roleName);

                return true;
            }
            return false;
        }

        public async Task<bool> CheckUserPasswordAsync(SystemUser user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> ConfirmUserEmailAsync(SystemUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(SystemUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var user = await FindUserByIdAsync(userId);

            if (user is not null)
            {
                _context.systemUsers.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }

            throw new IdNullException(nameof(userId));
        }

        public async Task<SystemUser> FindUserByEmailAsync(string email)
        {

            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        public async Task<SystemUser> FindUserByIdAsync(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            return result;
        }

        public async Task<SystemUser> FindUserByNameAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);

            if (result is null)
            {
                throw new IdNullException(nameof(userName));
            }
            return result;
        }

        public async Task<string> GenerateUserEmailConfirmationTokenAsync(SystemUser user)
        {
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return result;
        }

        public async Task<IEnumerable<SystemUser>> GetAllAsync()
        {
            var result = await _context.systemUsers.ToListAsync();

            return result;
        }

        public async Task<SystemUser> GetByIdAsync(string Id)
        {
            var result = await _context.systemUsers.Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (result is not null)
            {
                return result;
            }
            else
            {
                throw new IdNullException($"{result} is null");
            }

        }

        public async Task<IEnumerable<string>> GetUserMessages(string userId)
        {
            var result = await _context.Notifications.Where(n => n.receiverId == userId && n.isRead == false).Select(n => n.Message).ToListAsync();

            if (result.Count > 0)
            {
                return result;
            }

            return null;
        }

        public async Task<IList<string>> GetUserRolesAsync(SystemUser user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task MarkMessagesAsRead(string userId)
        {
            var notifications = await _context.
                Notifications
                .Where(n => n.receiverId == userId && n.isRead == false)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.isRead = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetMessagesCount(string userId)
        {
            var notifications = await _context.
                Notifications
                .Where(n => n.receiverId == userId && n.isRead == false).CountAsync();

            if (notifications > 0)
            {
                return notifications;
            }

            return 0;

        }
        public async Task<bool> SendToAllUsers(string message)
        {
            var users = await _context.Users.ToListAsync();


            foreach (var user in users)
            {
                var notification = new Notifications
                {
                    senderId = "e5e3313d-1d8c-4896-ae0b-2a0ad493aaa1",
                    receiverId = user.Id,
                    Message = message,
                    isRead = false,
                    CreatedOn = DateTime.UtcNow
                };

                await _context.Notifications.AddAsync(notification);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<SystemUser> UpdateAsync(SystemUser user)
        {
            var existingUser = await _context.systemUsers.FirstOrDefaultAsync(c => c.Id == user.Id);

            if (existingUser == null)
            {
                throw new ModelNullException($"{user.Id}", "Model is null");
            }

            existingUser.Name = user.Name;
            existingUser.UserName = user.Name;
            existingUser.PhoneNumber = user.PhoneNumber;

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task AddClaimsToUser(SystemUser user)
        {
            var claims = new List<Claim>();

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                if (role == Roles.Admin.ToString())
                {
                    claims.Add(new Claim("stadium:read", "true"));
                    claims.Add(new Claim("stadium:create", "true"));
                    claims.Add(new Claim("stadium:update", "true"));
                    claims.Add(new Claim("stadium:delete", "true"));
                }
                else if (role == Roles.Customer.ToString())
                {
                    claims.Add(new Claim("stadium:read", "true"));
                }
            }

            foreach (var claim in claims)
            {
                var hasClaim = await _userManager.GetClaimsAsync(user);
                if (!hasClaim.Any(c => c.Type == claim.Type))
                {
                    await _userManager.AddClaimAsync(user, claim);
                }
            }
        }

        public async Task<IList<Claim>> GetUserAssignedClaimsAsync(string userId)
        {
            var claims = await _context.UserClaims
                .Where(uc => uc.UserId == userId)
                .Join(_context.Claims,
                    uc => uc.ClaimId,
                    c => c.Id,
                    (uc, c) => new Claim(c.Type, "true"))
                .ToListAsync();

            return claims;
        }


    }

}
