using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Assets
{
    public sealed class AssetsByTickerRequest : PaginationRequest<string>
    {
        [FromRoute(Name = "ticker")]
        public string Ticker { get; set; }
    }
}
