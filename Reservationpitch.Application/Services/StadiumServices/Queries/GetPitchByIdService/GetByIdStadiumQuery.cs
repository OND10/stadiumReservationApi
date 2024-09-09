﻿using MediatR;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumByIdService
{
    public class GetByIdStadiumQuery : IQuery<StadiumResponseDto>
    {
        public Guid Id { get; set; }
    }
}
