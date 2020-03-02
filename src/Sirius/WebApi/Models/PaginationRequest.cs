using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models
{
    public class PaginationRequest<T>
    {
        [FromQuery(Name = "order")]
        public PaginationOrder Order { get; set; }

        [FromQuery(Name = "startingAfter")]
        public T StartingAfter { get; set; }

        [FromQuery(Name = "endingBefore")]
        public T EndingBefore { get; set; }

        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 25;
    }
}
