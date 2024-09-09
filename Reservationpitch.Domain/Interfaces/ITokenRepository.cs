using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTTokenAsync(SystemUser user, IEnumerable<string> roles);
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
