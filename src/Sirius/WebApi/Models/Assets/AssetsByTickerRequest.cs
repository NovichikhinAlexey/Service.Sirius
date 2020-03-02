using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Assets
{
    public sealed class AssetsByTickerRequest : PaginationRequest<string>
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }
     
        [FromRoute(Name = "ticker")]
        public string Ticker { get; set; }
    }
}
