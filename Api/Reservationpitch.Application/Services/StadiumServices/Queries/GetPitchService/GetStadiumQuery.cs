using MediatR;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumService
{
    public class GetStadiumQuery : IQuery<IEnumerable<StadiumResponseDto>>
    {
    }
}
