using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    //This is the bridge between your classes and the actual database 
    public class DatabaseContext : DbContext
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
            // configure this entity to have data
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = 3,
                    Name = "Romania",
                    ShortName = "RO"
                },
                new Country
                {
                    Id = 4,
                    Name = "Great Britain",
                    ShortName = "GB"
                });

            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and SPA",
                    Address = "Negril st.",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Paradise Hotel",
                    Address = "Main 23 st.",
                    CountryId = 2,
                    Rating = 4.8

                },
                new Hotel
                {
                    Id = 3,
                    Name = "London Hotel",
                    Address = "Queen  24 st.",
                    CountryId = 4,
                    Rating = 3.7

                },
                new Hotel
                {
                    Id = 4,
                    Name = "Casa Sipotelor",
                    Address = "Ucea de sus",
                    CountryId = 3,
                    Rating = 5

                });
        }
    }
}
