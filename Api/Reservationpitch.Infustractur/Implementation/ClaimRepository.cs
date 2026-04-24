using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Reservationpitch.Infustractur.Implementation
{
    internal class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<SystemUser> _userManager;

        public ClaimRepository(ApplicationDbContext context, UserManager<SystemUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AssignClaimsToUserAsync(string userId, List<Guid> claimIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            // Clean up existing DB links
            var existingClaims = await _context.UserClaims.Where(uc => uc.UserId == userId).ToListAsync();
            _context.UserClaims.RemoveRange(existingClaims);

            // Clear Identity claims
            var identityClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in identityClaims)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            // Assign selected claims
            foreach (var claimId in claimIds)
            {
                var claim = await _context.Claims.FindAsync(claimId);
                if (claim == null) continue;

                // DB link
                _context.UserClaims.Add(new UserClaimEntity
                {
                    UserId = userId,
                    ClaimId = claimId
                });

                // Identity claim
                await _userManager.AddClaimAsync(user, new Claim(claim.Value, "true")); // ✅ Type = claim.Value
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> GenerateAllClaimsAsync()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var controllerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            foreach (var controller in controllerTypes)
            {
                var controllerName = controller.Name.Replace("Controller", "").ToLower();
                var methods = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (var method in methods)
                {
                    var httpAttrs = method.GetCustomAttributes<HttpMethodAttribute>();
                    foreach (var attr in httpAttrs)
                    {
                        var claim = $"{controllerName}:{method.Name.ToLower()}";

                        var exists = await _context.Claims.AnyAsync(c => c.Value == claim);
                        if (!exists)
                        {
                            await _context.Claims.AddAsync(new ClaimEntity
                            {
                                Type = claim,     // ✅ both set to permission string
                                Value = claim
                            });
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<ClaimEntity>> GetAllClaimsAsync()
        {
            var claims = await _context.Claims.ToListAsync();
            return claims;
        }
    }
}
