using Autofac;
using Reservationpitch.Infustracture.Implementation;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Infustractur.Infustracture
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManagerRepository>().As<IUserManagerRepository>().InstancePerLifetimeScope();
        }
    }
}
