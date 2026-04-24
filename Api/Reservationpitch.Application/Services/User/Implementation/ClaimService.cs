using Microsoft.AspNetCore.Identity;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.Services.User.Interface;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.User.Implementation
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimService(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<Result<bool>> GenerateAllClaimsAsync()
        {
           var result = await _claimRepository.GenerateAllClaimsAsync();
            return await Result<bool>.SuccessAsync(result, "Claims generated successfully", true);

        }

        public async Task<Result<IEnumerable<ClaimEntity>>> GetAllClaimsAsync()
        {
            var claims = await _claimRepository.GetAllClaimsAsync();
            return await Result<IEnumerable<ClaimEntity>>.SuccessAsync(claims, "Fetched all claims", true);
        }

        public async Task<Result<bool>> AssignClaimsToUserAsync(string userId, List<Guid> claimIds)
        {
           var result = await _claimRepository.AssignClaimsToUserAsync(userId, claimIds);

            return await Result<bool>.SuccessAsync(result, "Claims assigned successfully", true);
        }
    }
}
