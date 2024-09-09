using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.UserDtos.Response
{
    public class LoginResponseDto
    {
        private UserResponseDto user { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }


        public UserResponseDto User
        {
            get
            {
                return new UserResponseDto
                {
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                };
            }
            set
            {
                user = value;
            }
        }
    }
}
