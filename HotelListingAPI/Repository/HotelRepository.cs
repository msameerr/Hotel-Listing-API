using HotelListingApi.Contracts;
using HotelListingAPI.Data;

namespace HotelListingApi.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext context) : base(context)
        {
        }
    }
}
