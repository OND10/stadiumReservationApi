using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.User.Interface
{
    public interface IClaimService
    {
        Task<Result<bool>> GenerateAllClaimsAsync();
        Task<Result<IEnumerable<ClaimEntity>>> GetAllClaimsAsync();
        Task<Result<bool>> AssignClaimsToUserAsync(string userId, List<Guid> claimIds);
    }
}
