﻿using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.Models.User
{
    public class ApiUserDto : LoginDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
       
    }

}
