using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.UserDtos.Request
{
    public record RefreshTokenRequestDto(string AccessToken, string RefreshToken);
    
}
