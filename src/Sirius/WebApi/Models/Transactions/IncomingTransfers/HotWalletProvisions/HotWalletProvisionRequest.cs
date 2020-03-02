using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Transactions.IncomingTransfers.HotWalletProvisions
{
    public sealed class HotWalletProvisionRequest : BlockchainNetworkEntitiesRequest
    {
        [FromQuery]
        public bool OnlyConfirmed { get; set; }

        [FromQuery]
        public bool OnlyIrreversible { get; set; }
    }
}
