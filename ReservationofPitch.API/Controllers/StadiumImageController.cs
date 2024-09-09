using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using Reservationpitch.Application.Services.StadiumImageServices.Commands.AddStadiumImageService;
using Reservationpitch.Application.Services.StadiumImageServices.Queries.GetStadiumImageByIdService;
using Reservationpitch.Application.Services.StadiumImageServices.Queries.GetStadiumImageService;
using Reservationpitch.Domain.Common;

namespace ReservationofPitch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumImageController : ControllerBase
    {
        private readonly ISender _sender;
        public StadiumImageController(ISender sender)
        {

            _sender = sender;
        }

        [HttpGet]
        public async Task<Result<IEnumerable<StadiumImageResponseDto>>> Get(CancellationToken cancellationToken)
        {
            var command = new GetStadiumImageQuery();
            var result = await _sender.Send(command, cancellationToken);
            //_handler.Handle(command, cancellationToken);
            return await Result<IEnumerable<StadiumImageResponseDto>>.SuccessAsync(result.Data, "Viewed Successfully", true);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<Result<IEnumerable<StadiumImageResponseDto>>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new GetByStadiumIdQuery
            {
                PitchId = id,
            };
            var result = await _sender.Send(command, cancellationToken);

            return await Result<IEnumerable<StadiumImageResponseDto>>.SuccessAsync(result.Data, " Viewed Successfully", true);
        }



        [HttpPost]
        public async Task<Result<IEnumerable<StadiumImageResponseDto>>> UploadImagesForPitch([FromForm] IEnumerable<IFormFile> files, [FromForm] Guid stadiumId, CancellationToken cancellationToken)
        {
            var pitchImages = new List<StadiumImageResponseDto>();

            foreach (var file in files)
            {
                var imgValidateResult = ImageValidation.ValidationFileUpload(file);
                if(imgValidateResult is false)
                {
                    return await Result<IEnumerable<StadiumImageResponseDto>>.FaildAsync(false, "Image size or format not matching");
                }
            }

                var command = new CreateStadiumImageCommand
                {
                    StadiumId = stadiumId,
                    Files = files
                };

                var result = await _sender.Send(command, cancellationToken);

            //pitchImages.Add(result.Data); // Assuming the Result class has a Data property


            return await Result<IEnumerable<StadiumImageResponseDto>>.SuccessAsync("Uploaded successfully", true);
        }

    }
}
