using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Transactions.OutgoingTransfers.Withdrawals
{
    public sealed class WithdrawalsRequest : BlockchainNetworkEntitiesRequest
    {
        [FromQuery]
        public WithdrawalState? State { get; set; }

        [FromQuery]
        public string BatchId { get; set; }

        [FromQuery]
        public string TransactionHash { get; set; }

        [FromQuery]
        public string HotWalletId { get; set; }

        [FromQuery]
        public string DestinationAddress { get; set; }

        [FromQuery]
        public string AssetId { get; set; }
    }
}
