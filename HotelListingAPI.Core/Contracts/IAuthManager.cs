using HotelListingApi.Data;
using HotelListingApi.Core.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListingApi.Core.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<AuthResponseDto> Login(LoginDto loginDto);


        Task<string> CreateRefreshToken(ApiUser user);
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);

    }
}
