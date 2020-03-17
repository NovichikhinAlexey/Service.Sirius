using System.Collections.Generic;
using System.Threading.Tasks;
using Sirius.Domain.DepositWallets;

namespace Sirius.Domain.Repositories
{
    public interface IDepositWalletRepository
    {
        Task AddOrUpdateAsync(DepositWallet depositWallet);
        Task<IReadOnlyCollection<DepositWallet>> GetManyAsync(string blockchainId, string networkId, int startAfter, int limit);
        Task<DepositWallet> GetByIdAsync(string blockchainId, string networkId, string walletId);
    }
}
