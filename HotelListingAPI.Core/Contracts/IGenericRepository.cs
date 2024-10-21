using HotelListingApi.Core.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelListingApi.Core.Contracts
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<bool> Exists(int id);

        Task<PageResult<TResult>> GetPagedAll<TResult>(QueryParameters queryParameters);


    }
}
