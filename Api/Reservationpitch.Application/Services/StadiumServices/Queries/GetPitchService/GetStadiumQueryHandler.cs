using AutoMapper;
using OnMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumService
{
    public class GetStadiumQueryHandler : IQueryHandler<GetStadiumQuery, IEnumerable<StadiumResponseDto>>
    {
        private readonly IStadiumRepository _pitchRepository;
        private readonly OnMapping _mapper;
        public GetStadiumQueryHandler(IStadiumRepository pitchRepository, OnMapping mapper)
        {
            _pitchRepository = pitchRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StadiumResponseDto>>> Handle(GetStadiumQuery request, CancellationToken cancellationToken)
        {
            var result = await _pitchRepository.GetAllAsync(cancellationToken);


            var mappedResult = await _mapper.MapCollection<Stadium, StadiumResponseDto>(result);

            //return await Task.FromResult<IEnumerable<PitchResponseDto>>(mappedResult);
            return await Result<IEnumerable<StadiumResponseDto>>.SuccessAsync(mappedResult.Data, "Viewed Successfully", true);
        }

    }
}
