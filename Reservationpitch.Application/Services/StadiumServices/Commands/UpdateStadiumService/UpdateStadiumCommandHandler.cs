using AutoMapper;
using MediatR;
using OnMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumServices.Commands.UpdateStadiumService
{
    public class UpdateStadiumCommandHandler : ICommandHandler<UpdateStadiumCommand, StadiumResponseDto>
    {
        private readonly OnMapping _mapper;
        private readonly IStadiumRepository _pitchRepository;
        public UpdateStadiumCommandHandler(IStadiumRepository pitchRepository, OnMapping mapper)
        {
            _mapper = mapper;
            _pitchRepository = pitchRepository;
        }

        public async Task<Result<StadiumResponseDto>> Handle(UpdateStadiumCommand request, CancellationToken cancellationToken)
        {
            // Mapping the request to the Model
            var model = await _mapper.Map<UpdateStadiumCommand,Stadium>(request);

            var result = await _pitchRepository.UpdateAsync(model.Data, cancellationToken);
            // Mapping the model to the Response
            var mappedResult = await _mapper.Map<Stadium, StadiumResponseDto>(result);

            return await Result<StadiumResponseDto>.SuccessAsync(mappedResult.Data, "Updated Successfully", true);
        }
    }
}
