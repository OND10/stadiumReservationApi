using MediatR;
using OnMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumCenterServices.Queries.CreateStadiumCenterById
{
    public class GetStadiumCenterByIdQueryHandler : IQueryHandler<GetStadiumCenterByIdQuery, StadiumCenterResponseDto>
    {
        private readonly IStadiumCenterRepository _repository;
        private readonly OnMapping _mapper;
        public GetStadiumCenterByIdQueryHandler(IStadiumCenterRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<Result<StadiumCenterResponseDto>> Handle(GetStadiumCenterByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id, cancellationToken);

            var response = await _mapper.Map<StadiumCenter, StadiumCenterResponseDto>(result);

            return await Result<StadiumCenterResponseDto>.SuccessAsync(response.Data, "Viewed Successfully", true);
        }

    }
}
