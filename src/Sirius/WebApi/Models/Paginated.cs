using System.Collections.Generic;

namespace Sirius.WebApi.Models
{
    public class Paginated<TItem, TId>
    {
        public Pagination<TId> Pagination { get; set; }
        public IReadOnlyCollection<TItem> Items { get; set; }
    }
}
