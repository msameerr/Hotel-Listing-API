using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Models.Hotel
{
    public class HotelDto : BaseHotelDto
    {
        public int HotelId { get; set; }

    }

}
