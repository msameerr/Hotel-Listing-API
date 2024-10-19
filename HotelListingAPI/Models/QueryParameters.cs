﻿namespace HotelListingApi.Models
{
    public class QueryParameters
    {

        private int _PageSize = 15;
        public int StartIndex { get; set; }

       public int PageSize
       {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
       }

    }
}
