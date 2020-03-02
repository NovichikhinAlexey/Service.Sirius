namespace Sirius.WebApi.Models.Transactions.OutgoingTransfers.WithdrawalBatches
{
    public sealed class BatchedWithdrawal
    {
        public string WithdrawalId { get; set; }
        public string Address { get; set; }
        public string AssetId { get; set; }
        public decimal Amount { get; set; }
        public string Tag { get; set; }
        public DestinationTagType? TagType { get; set; }
    }
}
