using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Core.Models.Hotel
{
    public class HotelDto : BaseHotelDto
    {
        public int HotelId { get; set; }

    }

}
