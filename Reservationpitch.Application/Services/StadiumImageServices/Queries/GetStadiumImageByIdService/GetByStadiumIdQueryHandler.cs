using AutoMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumImageServices.Queries.GetStadiumImageByIdService
{
    public class GetByStadiumIdQueryHandler : IQueryHandler<GetByStadiumIdQuery, IEnumerable<StadiumImageResponseDto>>
    {
        private readonly IStadiumImageRepository _repository;
        private readonly IMapper _mapper;
        public GetByStadiumIdQueryHandler(IStadiumImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StadiumImageResponseDto>>> Handle(GetByStadiumIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByStadiumIdAsync(request.PitchId, cancellationToken);

            var response = new StadiumImageResponseDto();

            var mappedResult = response.FromModel(result);

            return await Result<IEnumerable<StadiumImageResponseDto>>.SuccessAsync(mappedResult, "Found it Successfully", true);
        }
    }
}
