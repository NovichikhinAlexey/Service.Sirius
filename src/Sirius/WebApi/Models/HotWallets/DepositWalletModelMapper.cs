using Sirius.Domain.HotWallets;

namespace Sirius.WebApi.Models.HotWallets
{
    public static class HotWalletModelMapper
    {
        public static HotWalletModel MapFromDomain(HotWallet depositWallet)
        {
            if (depositWallet == null)
                return null;

            return new HotWalletModel()
            {
                Id = depositWallet.Id,
                PublicKey = depositWallet.PublicKey,
                GroupName = depositWallet.GroupName ?? "default",
                Address = depositWallet.Address
            };
        }
    }
}
