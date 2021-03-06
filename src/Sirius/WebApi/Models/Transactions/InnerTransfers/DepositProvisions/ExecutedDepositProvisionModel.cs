﻿using System.Collections.Generic;

namespace Sirius.WebApi.Models.Transactions.InnerTransfers.DepositProvisions
{
    public sealed class ExecutedDepositProvisionModel
    {
        // TODO: Should be sequential
        public string Id { get; set; }
        public string TransactionHash { get; set; }
        public string DepositWalletId { get; set; }
        public string DepositWalletAddress { get; set; }
        public string AssetId { get; set; }
        public decimal Amount { get; set; }
        public string HotWalletId { get; set; }
        public string HotWalletAddress { get; set; }
        public string GroupName { get; set; }
        public IReadOnlyCollection<PaidFee> TransactionFees { get; set; }
    }
}
