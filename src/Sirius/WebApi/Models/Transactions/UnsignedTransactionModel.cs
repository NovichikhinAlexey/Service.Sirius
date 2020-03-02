namespace Sirius.WebApi.Models.Transactions
{
    public sealed class UnsignedTransactionModel
    {
        // TODO: Should be sequential
        public string Id { get; set; }
        public int RetriesCount { get; set; }
        public string BuiltTransaction { get; set; }
    }
}
