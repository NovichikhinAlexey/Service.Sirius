using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.HotWallets
{
    public sealed class HotWalletsRequest : BlockchainNetworkEntitiesRequest
    {
        [FromQuery]
        public string GroupName { get; set; }
    }
}
