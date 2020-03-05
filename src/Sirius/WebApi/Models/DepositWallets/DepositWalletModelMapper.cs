using Sirius.Domain.Deposits;

namespace Sirius.WebApi.Models.DepositWallets
{
    public static class DepositWalletModelMapper
    {
        public static DepositWalletModel MapFromDomain(DepositWallet depositWallet)
        {
            if (depositWallet == null)
                return null;

            return new DepositWalletModel()
            {
                Id = depositWallet.Id,
                PublicKey = depositWallet.PublicKey,
                GroupName = depositWallet.GroupName ?? "default",
                Address = depositWallet.Address
            };
        }
    }
}
