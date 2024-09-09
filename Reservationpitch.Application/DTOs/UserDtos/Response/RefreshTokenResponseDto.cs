using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.UserDtos.Response
{
    public record RefreshTokenResponseDto(string Token, string RefreshToken);
    
}
