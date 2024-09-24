using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Reservationpitch.Domain.Common;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Infustractur.Shared.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageRepository(IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor)
        {
            _webHost = webHost;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Upload(object model, IFormFile file)
        {
            var orgModel = model.GetType();
            string modelName = orgModel.Name;
            //string abbreModelName = modelName.Substring(0, modelName.IndexOf('R'));


            //for(int i = 0; i<modelName.Length; i++)
            //{
            //    if (i == modelName.IndexOf('R')) 
            //    {
            //        abbreName = modelName.Substring(0, i);
            //        break;
            //    }
            //}

            ImageValidation.ValidationFileUpload(file);

            if (file != null)
            {
                // Uploading the image to the sepcified folder
                var folderPath = System.IO.Path.Combine(_webHost.ContentRootPath, "Images", $"User");
                var localPath = System.IO.Path.Combine(folderPath, file.FileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                using var stream = new FileStream(localPath, FileMode.Create);
                await file.CopyToAsync(stream);

                //Store filename and extenstion to the DB
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/User/{file.FileName}";


                return await Task.FromResult<string>(urlPath);
            }

            return await Task.FromResult<string>("");
        }
    }
}

