using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models
{
    public class BlockchainNetworkEntitiesRequest : PaginationRequest<string>
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }
    }
}
