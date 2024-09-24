using Microsoft.Extensions.DependencyInjection;
using OnMapper;
using Reservationpitch.Application.Services.Image.implementation;
using Reservationpitch.Application.Services.Image.Interface;
using Reservationpitch.Application.Services.User.Implementation;
using Reservationpitch.Application.Services.User.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Extensions
{
    public static class AddApplicationExtensision
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<OnMapping>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddMediatR(ctg =>
            {
                ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
