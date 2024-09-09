using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.StadiumDTOs.Request
{
    public class StadiumCenterRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Owned_By { get; set; } = string.Empty;
        public bool DressingRoomVisible { get; set; } = false;
        public string ImageUrl { get; set; }
        public IFormFile file {  get; set; }
    }
}
