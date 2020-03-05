using System;
using System.Collections.Generic;

namespace Sirius.WebApi.Models.Transactions.IncomingTransfers.Deposits
{
    public sealed class DepositModel
    {
        // TODO: Should be sequential
        public string Id { get; set; }
        public string TransactionHash { get; set; }
        public long ConfirmationsCount { get; set; }
        public long RequiredConfirmationsCount { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? ConfirmedDateTime { get; set; }
        public string AssetId { get; set; }
        public IReadOnlyCollection<IncomingTransferSource> Sources { get; set; }
        // either deposit wallet or hot wallet (in case if blockchain supports tags)
        public string DestinationAddress { get; set; }
        // null in case if blockchain supports tags
        public string DepositWalletId { get; set; }
        // user context associated with the deposit wallet or with the deposit tag (in case if blockchain supports tags)
        public string UserContext { get; set; }
        public string HotWalletId { get; set; }
        // null in case if blockchain doesn't support tags
        public string DepositWalletTag { get; set; }
        public string GroupName { get; set; }
        public DestinationTagType DestinationTagType { get; set; } 
        public IReadOnlyCollection<PaidFee> TransactionFees { get; set; }
    }
}
