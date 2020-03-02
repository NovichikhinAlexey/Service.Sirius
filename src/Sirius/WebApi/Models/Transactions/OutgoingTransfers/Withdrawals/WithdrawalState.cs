namespace Sirius.WebApi.Models.Transactions.OutgoingTransfers.Withdrawals
{
    public enum WithdrawalState
    {
        Started,
        Batched,
        Unsigned,
        Signed,
        Broadcasted,
        Accepted,
        Confirmed,
        Failed
    }
}
