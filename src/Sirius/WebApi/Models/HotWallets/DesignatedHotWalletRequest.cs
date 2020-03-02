using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.HotWallets
{
    public sealed class DesignatedHotWalletRequest
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }

        [FromQuery]
        public string GroupName { get; set; }
    }
}
