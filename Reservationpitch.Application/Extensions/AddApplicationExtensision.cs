using Microsoft.Extensions.DependencyInjection;
using OnMapper;
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
            services.AddMediatR(ctg =>
            {
                ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
