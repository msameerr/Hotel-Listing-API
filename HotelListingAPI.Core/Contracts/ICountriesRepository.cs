using HotelListingApi.Data;

namespace HotelListingApi.Core.Contracts
{
    public interface ICountriesRepository: IGenericRepository<Country>
    {

        Task<Country> GetDetails(int id);

    }
}
