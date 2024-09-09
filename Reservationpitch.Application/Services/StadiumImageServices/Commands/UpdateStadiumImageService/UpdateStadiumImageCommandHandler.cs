using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumImageServices.Commands.UpdateStadiumImageService
{
    public class UpdateStadiumImageCommandHandler : ICommandHandler<UpdateStadiumImageCommand, StadiumImageResponseDto>
    {
        private readonly IStadiumImageRepository _repository;
        public UpdateStadiumImageCommandHandler(IStadiumImageRepository repository)
        {
            _repository = repository;
        }
        public Task<Result<StadiumImageResponseDto>> Handle(UpdateStadiumImageCommand request, CancellationToken cancellationToken)
        {
            throw new Exception();
        }
    }
}
