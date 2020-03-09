using System;

namespace Sirius.Domain.Withdrawals
{
    public class ExecuteWithdrawal
    {
        public Guid RequestId { get; set; }
        public string BlockchainId { get; set; }
        public string NetworkId { get; set; }
        public string HotWalletAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string AssetId { get; set; }
        public decimal Amount { get; set; }
    }
}
