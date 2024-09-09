using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumReservationServices.Commands.CreateStadiumReservation
{
    public class CreateStadiumReservationCommand : ICommand<StadiumReservationResponseDto>
    {
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly DateTime { get; set; }
        public float Price { get; set; }
        public string Payment { get; set; }

        public StadiumReservation ToModel(CreateStadiumReservationCommand command)
        {
            return new StadiumReservation
            {
                UserName = command.UserName,
                UserPhoneNumber = command.UserPhoneNumber,
                Date = command.Date,
                Price = command.Price,
                Payment = command.Payment,

            };
        }
    }
}
