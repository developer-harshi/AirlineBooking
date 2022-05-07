using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPIServices.Entities;

namespace UserAPIServices.Context
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airline> Airline { get; set; }
        public DbSet<UserRegistrestion> UserRegistrestion { get; set; }
        public DbSet<FlightBooking> FlightBooking { get; set; }
        public DbSet<BookingPersons> BookingPersons { get; set; }
        public DbSet<Discount> Discount { get; set; }
    }
}
//add-migration userContextMigration
//update-database