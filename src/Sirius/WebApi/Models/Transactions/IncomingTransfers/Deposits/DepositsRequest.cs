using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Transactions.IncomingTransfers.Deposits
{
    public sealed class DepositsRequest : BlockchainNetworkEntitiesRequest
    {
        [FromQuery]
        public bool OnlyConfirmed { get; set; }

        [FromQuery]
        public bool OnlyIrreversible { get; set; }
    }
}
