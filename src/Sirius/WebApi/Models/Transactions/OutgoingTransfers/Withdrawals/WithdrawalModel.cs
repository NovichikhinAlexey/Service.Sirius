using System;
using System.Collections.Generic;
using Sirius.Domain;

namespace Sirius.WebApi.Models.Transactions.OutgoingTransfers.Withdrawals
{
    public sealed class WithdrawalModel
    {
        // TODO: Should be sequential
        public string Id { get; set; }
        public string BatchId { get; set; }
        public string TransactionHash { get; set; }
        public long ConfirmationsCount { get; set; }
        public bool IsConfirmed { get; set; }
        public IReadOnlyCollection<PaidFee> PaidFees { get; set; }
        public string HotWalletId { get; set; }
        public string HotWalletAddress { get; set; }
        public string GroupName { get; set; }
        public string AssetId { get; set; }
        public decimal Amount { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationTag { get; set; }
        public DestinationTagType? DestinationTagType { get; set; }
        public FeePayer FeePayer { get; set; }
        public FeeStrategy FeeStrategy { get; set; }
        public bool SkipBatching { get; set; }
        public WithdrawalState State { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime StartedDateTime { get; set; }
        public DateTime? BatchedDateTime { get; set; }
        public DateTime? BuiltDateTime { get; set; }
        public DateTime? SignedDateTime { get; set; }
        public DateTime? BroadcastedDateTime { get; set; }
        public DateTime? AcceptedDateTime { get; set; }
        public DateTime? ConfirmedDateTime { get; set; }
        public DateTime? FailedDateTime { get; set; }
        public int RetriesCount { get; set; }
    }
}
