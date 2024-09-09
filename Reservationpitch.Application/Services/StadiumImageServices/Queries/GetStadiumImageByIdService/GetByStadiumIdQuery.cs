using MediatR;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumImageServices.Queries.GetStadiumImageByIdService
{
    public class GetByStadiumIdQuery : IQuery<IEnumerable<StadiumImageResponseDto>>
    {
        public Guid PitchId { get; set; }
    }
}
