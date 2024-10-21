namespace HotelListingApi.Core.Models
{
    public class PageResult<T>
    {

        public int TotalCount { get; set; }
        public int RecordNumber { get; set; }
        public List<T> Items { get; set; }

    }
}
