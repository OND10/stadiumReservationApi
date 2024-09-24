
using Microsoft.AspNetCore.Http;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.Services.Image.Interface;
using Reservationpitch.Domain.Interface;
using Reservationpitch.Domain.Shared;

namespace Reservationpitch.Application.Services.Image.implementation
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _repository;
        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> UploadImage(object model, IFormFile file)
        {
            var result = await _repository.Upload(model, file);

            return await Result<string>.SuccessAsync(result, ResponseStatus.UploadedSuccess, true);
        }
    }
}
