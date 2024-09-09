using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumImageServices.Queries.GetStadiumImageService
{
    public class GetStadiumImageQuery : ICommand<IEnumerable<StadiumImageResponseDto>>
    {

    }
}
