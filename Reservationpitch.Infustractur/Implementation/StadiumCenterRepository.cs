using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reservationpitch.Domain.Common;
using Reservationpitch.Domain.Common.Exceptions;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Infustractur.Implementation
{
    public class StadiumCenterRepository : IStadiumCenterRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StadiumCenterRepository(ApplicationDbContext context, IWebHostEnvironment webHost, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHost = webHost;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<StadiumCenter> CreateAsync(StadiumCenter entity, CancellationToken cancellationToken)
        {
            try
            {
                await _context.StadiumCenters.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new ModelNullException($"{entity}", " Exception : StadiumCenter is null");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var stadiumCenter = await _context.StadiumCenters.Where(s => s.Id == id).FirstOrDefaultAsync();

            if (stadiumCenter is not null)
            {
                _context.StadiumCenters.Remove(stadiumCenter);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<IEnumerable<StadiumCenter>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _context.StadiumCenters.ToListAsync();

            if (list.Count > 0)
            {
                return list;
            }

            return Enumerable.Empty<StadiumCenter>();
        }

        public async Task<StadiumCenter> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.StadiumCenters.FindAsync(id);

            if (result is not null)
            {
                return result;
            }

            throw new IdNullException("Id is null");
        }

        public async Task<StadiumCenter> UpdateAsync(StadiumCenter entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Upload(StadiumCenter model, IFormFile file)
        {
            var orgModel = model.GetType();
            string modelName = orgModel.Name;

            bool validate = ImageValidation.ValidationFileUpload(file);

            if (file != null && validate)
            {
                // Uploading the image to the sepcified folder
                var folderPath = System.IO.Path.Combine(_webHost.ContentRootPath, "Images", $"{modelName}");
                var localPath = System.IO.Path.Combine(folderPath, file.FileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                using var stream = new FileStream(localPath, FileMode.Create);
                await file.CopyToAsync(stream);

                //Store filename and extenstion to the DB
                var httpRequest = _httpContextAccessor.HttpContext.Request;
                var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{modelName}/{file.FileName}";


                return await Task.FromResult<string>(urlPath);
            }

            return await Task.FromResult<string>("");
        }


    }
}
