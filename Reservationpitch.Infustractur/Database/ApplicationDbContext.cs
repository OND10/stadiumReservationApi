using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Infustractur.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Stadium> Stadiums {  get; set; }
        public DbSet<StadiumImages> StadiumImages {  get; set; }
        public DbSet<StadiumCenter> StadiumCenters {  get; set; }
        public DbSet<StadiumReservation> StadiumReservations {  get; set; }
        public DbSet<SystemUser> systemUsers { get; set; }
        public DbSet<WorkDays> WorkDays { get; set; }
    }
}
