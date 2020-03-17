using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.BlockchainWalletApi.Client.Http;
using Sirius.Domain.Repositories;

namespace Sirius.Domain.HotWallets
{
    public class HotWalletService
    {
        private readonly IBlockchainWalletClient _blockchainWalletClient;
        private readonly IHotWalletRepository _hotWalletRepository;

        public HotWalletService(
            IBlockchainWalletClient blockchainWalletClient,
            IHotWalletRepository hotWalletRepository)
        {
            _blockchainWalletClient = blockchainWalletClient;
            _hotWalletRepository = hotWalletRepository;
        }

        public async Task<HotWallet> DesignateWalletAsync(
            string blockchainId,
            string networkId,
            string groupName,
            string id)
        {
            var hotwallet = await GetHotWalletAsync(blockchainId, networkId, id);

            if (hotwallet == null)
                return null;

            await _hotWalletRepository.DesignateAsync(blockchainId, networkId, groupName, id);

            return hotwallet;
        }

        public async Task<HotWallet> GetDesignatedWalletAsync(
            string blockchainId,
            string networkId,
            string groupName)
        {
            var hotWallet = await _hotWalletRepository.GetDesignatedAsync(blockchainId, networkId, groupName);

            return hotWallet;
        }

        public async Task<HotWallet> ImportAsync(
            string blockchainId, 
            string networkId, 
            string address, 
            string groupName, 
            string pubKey = null)
        {
            var importedWallet = await _blockchainWalletClient.ImportWalletAsync(blockchainId, networkId, new ImportWalletRequest()
            {
                Address = address,
                PubKey = pubKey,
                TransferCallbackOptions = new TransferCallbackOptions()
            });

            if (importedWallet == null)
            {
                return null;
            }

            var hotWallet = new HotWallet
            {
                Address = address,
                BlockchainId = blockchainId,
                GroupName = groupName,
                Id = importedWallet.Id.ToString(),
                NetworkId = networkId,
                PublicKey = pubKey
            };

            await _hotWalletRepository.AddOrUpdateAsync(hotWallet);

            return hotWallet;
        }

        public async Task<IReadOnlyCollection<HotWallet>> GetAllAsync(
            string blockchainId,
            string networkId,
            int startAfter,
            int endBefore,
            int limit
            )
        {
            var result = await _hotWalletRepository.GetAllAsync(blockchainId, networkId, startAfter, limit);

            return result;
        }


        public Task<HotWallet> GetByIdAsync(
            string blockchainId,
            string networkId,
            string walletId)
        {
            return GetHotWalletAsync(blockchainId, networkId, walletId);
        }

        private Task<HotWallet> GetHotWalletAsync(string blockchainId, string networkId, string walletId)
        {
            return _hotWalletRepository.GetAsync(blockchainId, networkId, walletId);
        }
    }
}
