using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.Core.Models.Country;
using HotelListingApi.Core.Models.Hotel;
using HotelListingApi.Core.Models.User;
using HotelListingApi.Data;


namespace HotelListingApi.Core.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();

            CreateMap<ApiUser, ApiUserDto>().ReverseMap();
        }

    }
}
