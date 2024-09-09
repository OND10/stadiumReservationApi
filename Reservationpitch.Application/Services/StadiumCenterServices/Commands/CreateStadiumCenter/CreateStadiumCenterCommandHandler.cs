using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumCenterServices.Commands.CreateStadiumCenter
{
    public class CreateStadiumCenterCommandHandler : ICommandHandler<CreateStadiumCenterCommand, StadiumCenterResponseDto>
    {
        private readonly IStadiumCenterRepository _repository;

        public CreateStadiumCenterCommandHandler(IStadiumCenterRepository repository)
        {
            _repository = repository;
        }


        public async Task<Result<StadiumCenterResponseDto>> Handle(CreateStadiumCenterCommand command, CancellationToken cancellationToken)
        {
            var model = command.ToModel(command);

            model.ImageUrl = await _repository.Upload(model, command.file);

            var result = await _repository.CreateAsync(model, cancellationToken);

            var response = new StadiumCenterResponseDto();

            var mappedResponse = response.FromModel(result);

            return await Result<StadiumCenterResponseDto>.SuccessAsync(mappedResponse, "Created Successfully", true);
        }
    }
}
