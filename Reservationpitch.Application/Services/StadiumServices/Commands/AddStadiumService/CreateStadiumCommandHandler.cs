using AutoMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumServices.Commands.AddStadiumService
{
    public class CreateStadiumCommandHandler : ICommandHandler<CreateStadiumCommand, StadiumResponseDto>
    {
        private readonly IStadiumRepository _pitchRepository;
        private readonly IMapper _mapper;

        public CreateStadiumCommandHandler(IStadiumRepository pitchRepository, IMapper mapper)
        {
            _pitchRepository = pitchRepository;
            _mapper = mapper;
        }

        public async Task<Result<StadiumResponseDto>> Handle(CreateStadiumCommand request, CancellationToken cancellationToken)
        {
            //Mapping your request to Model
            var model = new Stadium()
            {
                Name = request.Name,
                NoOfplayers = request.NoOfplayers,
                PriceinHour = request.PriceinHour,
                Type = request.Type,
                stadiumCenterId = request.stadiumCenterId
            };

            var result = await _pitchRepository.CreateAsync(model, cancellationToken);

            //Mapping your model to Response
            var mappedResult = _mapper.Map<StadiumResponseDto>(result);

            return await Result<StadiumResponseDto>.SuccessAsync(mappedResult, "Added Successfully", true);
        }
    }
}
