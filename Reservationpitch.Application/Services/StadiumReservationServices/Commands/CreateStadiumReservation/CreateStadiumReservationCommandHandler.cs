using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumReservationServices.Commands.CreateStadiumReservation
{
    public class CreateStadiumReservationCommandHandler : ICommandHandler<CreateStadiumReservationCommand, StadiumReservationResponseDto>
    {
        private readonly IStadiumReservationRepository _repository;

        public CreateStadiumReservationCommandHandler(IStadiumReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<StadiumReservationResponseDto>> Handle(CreateStadiumReservationCommand request, CancellationToken cancellationToken)
        {
            var model = request.ToModel(request);
            
            var result = await _repository.CreateAsync(model, cancellationToken);

            var response = new StadiumReservationResponseDto();

            var mappedResponse = response.FromModel(result);

            return await Result<StadiumReservationResponseDto>.SuccessAsync(mappedResponse, "Created Successfully", true);
        }
    }
}
