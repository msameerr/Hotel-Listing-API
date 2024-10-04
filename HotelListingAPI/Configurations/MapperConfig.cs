using AutoMapper;
using HotelListingApi.Models.Country;
using HotelListingAPI.Data;


namespace HotelListingApi.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
        }

    }
}
