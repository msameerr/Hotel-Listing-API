using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Data
{
    public class Hotel
    {

        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }


        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public int CountryId { get; set; }

    }
}
