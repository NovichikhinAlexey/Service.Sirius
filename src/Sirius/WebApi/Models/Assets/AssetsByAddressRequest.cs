using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Assets
{
    public sealed class AssetsByAddressRequest
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }
     
        [FromRoute(Name = "address")]
        public string Address { get; set; }
    }
}
