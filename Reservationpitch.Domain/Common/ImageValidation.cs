using Microsoft.AspNetCore.Http;
using Reservationpitch.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Common
{
    public static class ImageValidation
    {
        public static bool ValidationFileUpload(IFormFile file)
        {
            var preferedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!preferedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                return false;
            }

            if (file.Length > 10485760)
            {
                return false;
            }
            return true;
        }

    }
}
