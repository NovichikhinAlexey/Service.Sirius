namespace Sirius.WebApi.Models
{
    public class Pagination<T>
    {
        public T EndingBefore { get; set; }
        public T StartingAfter { get; set; }
        public int Count { get; set; }
        public PaginationOrder Order { get; set; }
        public string PrevUrl { get; set; }
        public string NextUrl { get; set; }
    }
}
