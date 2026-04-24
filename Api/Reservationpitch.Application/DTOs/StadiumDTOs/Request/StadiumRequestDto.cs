using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.StadiumDTOs.Request
{
    public class StadiumRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public double PriceinHour { get; set; }
        public string Type { get; set; } = string.Empty;
        public string NoOfplayers { get; set; } = null!;
        public Guid stadiumCenterId { get; set; }
    }
}
