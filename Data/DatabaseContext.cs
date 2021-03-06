using HotelListing.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    //This is the bridge between your classes and the actual database 
    public class DatabaseContext : IdentityDbContext<ApiUser> 
    { 
        // initialize the base contructor to take the same options
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        // it's here where we list out what the database should know about when it's been generated
        // DbSet<DataType> = your table , make the entity plural if needed Countries = the name of the table in the db
        // DbSet = a set of countries
        public DbSet<Country> Countries { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        //override the OnModelCreating method from the DbContext base class:
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());
        }
    }
}
