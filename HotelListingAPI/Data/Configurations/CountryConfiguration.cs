using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.Data;

namespace HotelListingApi.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(

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
        }
    }
}
