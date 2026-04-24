using Microsoft.AspNetCore.Http;
using Reservationpitch.Application.Common.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.Image.Interface
{
    public interface IImageService
    {
        Task<Result<string>> UploadImage(object model, IFormFile file);
    }
}
