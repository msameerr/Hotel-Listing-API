using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HotelListingAPI.Data
{
    public class HotelListingDbContext : DbContext
    {

        public HotelListingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().HasData(

                new Country
                {
                    Id = 1,
                    Name = "Pakistan",
                    ShortName = "PK"
                },
                new Country
                {
                    Id = 2,
                    Name = "United Kingdom",
                    ShortName = "UK"
                },
                new Country
                {
                    Id = 3,
                    Name = "United States Of America",
                    ShortName = "USA"
                },
                new Country
                {
                    Id = 4,
                    Name = "Germany",
                    ShortName = "Ger"
                }

            );

            modelBuilder.Entity<Hotel>().HasData(

                new Hotel
                {
                    HotelId = 1,
                    Name = "Hotel Farhan",
                    Address = "Karachi",
                    CountryId = 1,
                    Rating = 4.2
                },
                new Hotel
                {
                    HotelId = 2,
                    Name = "Hotel of US",
                    Address = "Newyork",
                    CountryId = 3,
                    Rating = 4
                },
                new Hotel
                {
                    HotelId = 3,
                    Name = "Hotel UK",
                    Address = "London",
                    CountryId = 2,
                    Rating = 4.5
                },
                new Hotel
                {
                    HotelId = 4,
                    Name = "Hotel Germany",
                    Address = "frankFurt",
                    CountryId = 4,
                    Rating = 4.3
                }

            );
        }


    }
}

