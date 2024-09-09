using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Shared
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int AccessTokenExpiration { get; set; } = 5; // in minutes
        public int RefreshTokenExpiration { get; set; } = 7; // in days
    }
}
