using Reservationpitch.Domain.Interface;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Database;

namespace Reservationpitch.Infustractur.Implementation
{
    public class UnitofWork : IUnitofWork
    {


        private readonly IStadiumImageRepository stadiumImageRepository; 
        private readonly IImageRepository imageRepository; 
        private readonly IStadiumCenterRepository stadiumCenterRepository; 
        private readonly IStadiumRepository stadiumRepository; 
        private readonly ITokenRepository tokenRepository;
        private readonly IUserManagerRepository userManagerRepository;
        private readonly ApplicationDbContext _context;

        public UnitofWork(IStadiumImageRepository stadiumImageRepository, 
            IImageRepository imageRepository, 
            IStadiumCenterRepository stadiumCenterRepository, 
            IStadiumRepository stadiumRepository, 
            ITokenRepository tokenRepository, 
            IUserManagerRepository userManagerRepository,
            ApplicationDbContext context)
        {
            this.stadiumImageRepository = stadiumImageRepository;
            this.imageRepository = imageRepository;
            this.stadiumCenterRepository = stadiumCenterRepository;
            this.stadiumRepository = stadiumRepository;
            this.tokenRepository = tokenRepository;
            this.userManagerRepository = userManagerRepository;
            _context = context;
        }


        public IStadiumImageRepository StadiumImageRepository => stadiumImageRepository ;

        public IImageRepository ImageRepository => imageRepository ;

        public IStadiumCenterRepository StadiumCenterRepository =>  stadiumCenterRepository;

        public IStadiumRepository StadiumRepository => stadiumRepository ;

        public ITokenRepository TokenRepository => tokenRepository ;

        public IUserManagerRepository UserManagerRepository => userManagerRepository ;

        public void Dispose()
        {
             
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
