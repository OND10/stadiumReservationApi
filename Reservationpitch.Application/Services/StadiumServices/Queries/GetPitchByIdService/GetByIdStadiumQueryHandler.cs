using AutoMapper;
using MediatR;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumByIdService
{
    public class GetByIdStadiumQueryHandler : IQueryHandler<GetByIdStadiumQuery, StadiumResponseDto>
    {
        private readonly IStadiumRepository _pitchRepository;
        private readonly IMapper _mapper;
        public GetByIdStadiumQueryHandler(IStadiumRepository pitchRepository, IMapper mapper)
        {
            _pitchRepository = pitchRepository;
            _mapper = mapper;
        }

        public async Task<Result<StadiumResponseDto>> Handle(GetByIdStadiumQuery request, CancellationToken cancellationToken)
        {
            var result = await _pitchRepository.FindAsync(p => p.Id == request.Id, cancellationToken);

            var mappedResult = _mapper.Map<StadiumResponseDto>(result);

            return await Result<StadiumResponseDto>.SuccessAsync(mappedResult, "Found it Successfully", true);
        }
    }
}
