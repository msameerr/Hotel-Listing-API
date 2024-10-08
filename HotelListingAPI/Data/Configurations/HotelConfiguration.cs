using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.Data;

namespace HotelListingApi.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(

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
