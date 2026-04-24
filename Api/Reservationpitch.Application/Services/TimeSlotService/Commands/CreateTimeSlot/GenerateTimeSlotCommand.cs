using Reservationpitch.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservationpitch.Application.Services.TimeSlotService.Commands.CreateTimeSlot
{
    public class GenerateTimeSlotCommand : ICommand<bool>
    {
        public Guid CenterId { get; set; }
    }
}
