using Reservationpitch.Application.Abstractions.Messaging;

namespace Reservationpitch.Application.Services.StadiumServices.Commands.DeleteStadiumService
{
    public class DeleteStadiumCommand : ICommand<bool>
    {
        public Guid Id { get; set; }
    }
}
