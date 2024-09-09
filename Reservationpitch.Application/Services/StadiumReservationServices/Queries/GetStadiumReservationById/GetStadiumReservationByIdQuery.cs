using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumReservationServices.Queries.GetStadiumReservationById
{
    public class GetStadiumReservationByIdQuery : IQuery<StadiumReservationResponseDto>
    {
        public Guid Id { get; set; }
    }
}
