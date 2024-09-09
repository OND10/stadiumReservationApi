using AutoMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumImageServices.Commands.AddStadiumImageService
{
    public class CreateStadiumImageCommandHandler : ICommandHandler<CreateStadiumImageCommand, StadiumImageResponseDto>
    {
        private readonly IStadiumImageRepository _repository;
        private readonly IMapper _mapper;
        public CreateStadiumImageCommandHandler(IStadiumImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<StadiumImageResponseDto>> Handle(CreateStadiumImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.UploadAsync(request.Files, request.StadiumId, "");
            //var mappedResult = _mapper.Map<PitchImageResponseDto>(result);
            var mappedResult = new StadiumImageResponseDto
            {
                CreatedOn = result.CreatedOn,
                FileName = result.FileName,
                ImageUrl = result.ImageUrl,
                StadiumId = result.StadiumId,
            };

            return await Result<StadiumImageResponseDto>.SuccessAsync(mappedResult, "Uploaded Successfully", true);
        }
    }
}
