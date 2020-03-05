using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models
{
    public sealed class BlockchainNetworkEntityRequest
    {
        [FromRoute(Name = "id")]
        public string Id { get; set; }
    }
}
