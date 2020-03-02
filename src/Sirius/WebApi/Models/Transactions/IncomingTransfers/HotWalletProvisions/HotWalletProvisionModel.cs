using System;
using System.Collections.Generic;

namespace Sirius.WebApi.Models.Transactions.IncomingTransfers.HotWalletProvisions
{
    public sealed class HotWalletProvisionModel
    {
        // TODO: Should be sequential
        public string Id { get; set; }
        public string TransactionHash { get; set; }
        public long ConfirmationsCount { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? ConfirmedDateTime { get; set; }
        public string AssetId { get; set; }
        public IReadOnlyCollection<IncomingTransferSource> Sources { get; set; }
        public string HotWalletAddress { get; set; }
        public string HotWalletId { get; set; }
        public string GroupName { get; set; }
        public IReadOnlyCollection<PaidFee> PaidFees { get; set; }
    }
}
