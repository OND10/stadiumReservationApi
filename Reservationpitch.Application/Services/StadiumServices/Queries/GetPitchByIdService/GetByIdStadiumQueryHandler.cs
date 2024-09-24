using AutoMapper;
using MediatR;
using OnMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
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
        private readonly OnMapping _mapper;
        public GetByIdStadiumQueryHandler(IStadiumRepository pitchRepository, OnMapping mapper)
        {
            _pitchRepository = pitchRepository;
            _mapper = mapper;
        }

        public async Task<Result<StadiumResponseDto>> Handle(GetByIdStadiumQuery request, CancellationToken cancellationToken)
        {
            var result = await _pitchRepository.FindAsync(p => p.Id == request.Id, cancellationToken);

            var mappedResult = await _mapper.Map<Stadium,StadiumResponseDto>(result);

            return await Result<StadiumResponseDto>.SuccessAsync(mappedResult.Data, "Found it Successfully", true);
        }
    }
}
