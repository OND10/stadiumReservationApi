using CMS.Infustracture.Implementation;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservationpitch.Domain.Interface;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Domain.Shared;
using Reservationpitch.Infustractur.Database;
using Reservationpitch.Infustractur.Email;
using Reservationpitch.Infustractur.Implementation;
using Reservationpitch.Infustractur.Shared.Implementation;

namespace Reservationpitch.Infustractur.Extensions
{
    public static class AddPresistenceExtensision
    {
        public static IServiceCollection AddPresistence(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddTransient<DapperDbContext>();
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("defualtConnection"));
            //});
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IStadiumImageRepository, StadiumImageRepository>();
            services.AddScoped<IStadiumRepository, StadiumRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IStadiumCenterRepository, StadiumCenterRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IStadiumReservationRepository, StadiumReservationRepository>();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
