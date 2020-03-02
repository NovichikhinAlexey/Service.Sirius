using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.DepositWallets
{
    public sealed class DepositWalletsRequest : BlockchainNetworkEntitiesRequest
    {
        [FromQuery]
        public string GroupName { get; set; }
    }
}
