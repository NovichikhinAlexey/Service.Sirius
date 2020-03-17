using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sirius.Domain.HotWallets;

namespace Sirius.Domain.Repositories
{
    public interface IHotWalletRepository
    {
        Task DesignateAsync(string blockchainId, string networkId, string groupName, string id);
        Task<HotWallet> GetDesignatedAsync(string blockchainId, string networkId, string groupName);
        Task AddOrUpdateAsync(HotWallet hotWallet);
        Task<IReadOnlyCollection<HotWallet>> GetAllAsync(string blockchainId, string networkId, int startAfter, int limit);
        Task<HotWallet> GetAsync(string blockchainId, string networkId, string walletId);
    }
}
