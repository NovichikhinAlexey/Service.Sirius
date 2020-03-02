using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Networks
{
    public sealed class NetworksRequest : PaginationRequest<string>
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }
    }
}
