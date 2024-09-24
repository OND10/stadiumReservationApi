using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using Reservationpitch.Infustractur.Database;
using Microsoft.EntityFrameworkCore;

namespace Reservationpitch.Infustractur.Implementation
{
    public class StadiumImageRepository :  IStadiumImageRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public StadiumImageRepository(
            ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            //_webHost = webHost;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public Task<IEnumerable<StadiumImages>> FindAllAsync(Expression<Func<StadiumImages, bool>> expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StadiumImages>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _context.StadiumImages.ToListAsync();
            if(result.Count > 0)
            {
                return result;
            }

            return Enumerable.Empty<StadiumImages>(); 
        }

        public async Task<StadiumImages> UploadAsync(IEnumerable<IFormFile> files, Guid stadiumId, string rootPath)
        {
            StadiumImages uploadedImage = null;

            foreach (var file in files)
            {
                var model = new StadiumImages
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = file.FileName,
                    CreatedOn = DateTime.Now,
                    Title = "Image",
                    StadiumId = stadiumId
                };

                //Split Filename from the extension
                var splittedFilename = model.FileName.Split('.')[0];
                // Uploading the image to the specified folder
                var localpath = Path.Combine(rootPath, "Images", $"{splittedFilename}{model.FileExtension}");
                using var stream = new FileStream(localpath, FileMode.Create);
                await file.CopyToAsync(stream);

                // Store filename and extension to the DB
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{model.FileName}";

                model.ImageUrl = urlPath;
                await _context.StadiumImages.AddAsync(model);
                uploadedImage = model;
            }

            await _context.SaveChangesAsync();

            return await Task.FromResult<StadiumImages>(uploadedImage);

        }

        public async Task<IEnumerable<StadiumImages>> GetByStadiumIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var findIdResult = await _context.Set<StadiumImages>().Where(u=> u.StadiumId == id).ToListAsync();
            if (findIdResult.Count > 0)
            {
                return findIdResult;
            }
            else
            {
                return Enumerable.Empty<StadiumImages>();
            }
        }
    }
}
