using AdminAPIServices.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPIServices.Context
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airline> Airline { get; set; }
        public DbSet<UserRegistrestion> UserRegistrestion { get; set; }
        public DbSet<Discount> Discount { get; set; }
    }
}
//add-migration AdminContextMigration
//update-database