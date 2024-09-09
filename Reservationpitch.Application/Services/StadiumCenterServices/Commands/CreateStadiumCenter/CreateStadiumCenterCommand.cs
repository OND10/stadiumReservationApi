using Microsoft.AspNetCore.Http;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumCenterServices.Commands.CreateStadiumCenter
{
    public class CreateStadiumCenterCommand : ICommand<StadiumCenterResponseDto>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Owned_By { get; set; } = string.Empty;
        public bool DressingRoomVisible { get; set; } = false;
        public string ImageUrl { get; set; }
        public IFormFile file { get; set; }

        public StadiumCenter ToModel(CreateStadiumCenterCommand command)
        {
            return new StadiumCenter
            {
                Name = command.Name,
                PhoneNumber = command.PhoneNumber,
                Location = command.Location,
                DressingRoomVisible = command.DressingRoomVisible,
                Owned_By = command.Owned_By,
                ImageUrl = command.ImageUrl,
            };
        }
    }
}
