using HotelListingApi.Core.Models.Hotel;

namespace HotelListingApi.Core.Models.Country
{
    public class CountryDto : BaseCountryDto
    {
        public int Id { get; set; }

        public List<HotelDto> Hotels { get; set; }
    }
}
