using System.Collections.Generic;

namespace Sirius.WebApi.Models.Transactions.OutgoingTransfers.WithdrawalBatches
{
    public sealed class ExecutedWithdrawalBatchModel
    {
        // TODO: Should be sequential
        public string Id { get; set; }
        public string TransactionHash { get; set; }
        public string HotWalletId { get; set; }
        public string HotWalletAddress { get; set; }
        public string GroupName { get; set; }
        public IReadOnlyCollection<BatchedWithdrawal> Withdrawals { get; set; }
        public IReadOnlyCollection<PaidFee> PaidFees { get; set; }
    }
}
