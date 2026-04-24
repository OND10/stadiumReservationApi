using MediatR;
using Microsoft.AspNetCore.Http;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumImageServices.Commands.AddStadiumImageService
{
    public class CreateStadiumImageCommand : ICommand<StadiumImageResponseDto>
    {
        public Guid StadiumId { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
