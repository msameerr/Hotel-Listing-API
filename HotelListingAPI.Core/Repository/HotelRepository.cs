using AutoMapper;
using HotelListingApi.Core.Contracts;
using HotelListingApi.Data;

namespace HotelListingApi.Core.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelListingDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
