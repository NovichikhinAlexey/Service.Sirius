﻿namespace Sirius.WebApi.Models.Transactions
{
    public sealed class PaidFee
    {
        public string AssetId { get; set; }
        public decimal Amount { get; set; }
    }
}
