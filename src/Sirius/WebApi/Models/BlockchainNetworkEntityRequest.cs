using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models
{
    public sealed class BlockchainNetworkEntityRequest
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }

        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
