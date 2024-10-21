using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.Core.Models.User
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
