using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IClaimRepository
    {
        Task<bool> GenerateAllClaimsAsync();
        Task<IEnumerable<ClaimEntity>> GetAllClaimsAsync();
        Task<bool> AssignClaimsToUserAsync(string userId, List<Guid> claimIds);
    }
}
