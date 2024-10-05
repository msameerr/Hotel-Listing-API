﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Models.Hotel
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        public int CountryId { get; set; }
    }
}
