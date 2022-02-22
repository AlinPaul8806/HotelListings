using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel { 
                    Id = 1,
                    Name = "Sandals Resort and SPA",
                    Address = "Negril st.",
                    Rating = 4.5,
                    CountryId = 1
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Paradise Hotel",
                    Address = "Main 23 st.",
                    Rating = 4.8,
                    CountryId = 2
                },
                new Hotel 
                {
                    Id = 3,
                    Name = "London Hotel",
                    Address = "Queen  24 st.",
                    Rating = 3.7,
                    CountryId = 4
                },
                new Hotel
                {
                    Id = 4,
                    Name = "Casa Sipotelor",
                    Address = "Ucea de sus",
                    Rating = 5,
                    CountryId = 3
                });
        }
    }
}
