using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.UserDtos.Response
{
    public class UserResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string NoneHashedPassword {  get; set; } = string.Empty;
        public string ImageUrl {  get; set; } = string.Empty;

    }
}
