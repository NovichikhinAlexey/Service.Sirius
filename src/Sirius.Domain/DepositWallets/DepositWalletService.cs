using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.BlockchainWalletApi.Client.Http;
using Sirius.Domain.Repositories;

namespace Sirius.Domain.DepositWallets
{
    public class DepositWalletService
    {
        private readonly ConcurrentDictionary<(string BlockchainId, string NetworkId), ConcurrentDictionary<string, DepositWallet>> _wallets;
        private readonly IBlockchainWalletClient _blockchainWalletClient;
        private readonly IDepositWalletRepository _depositWalletRepository;

        public DepositWalletService(
            IBlockchainWalletClient blockchainWalletClient,
            IDepositWalletRepository depositWalletRepository)
        {
            _blockchainWalletClient = blockchainWalletClient;
            _depositWalletRepository = depositWalletRepository;
            _wallets = new ConcurrentDictionary<(string BlockchainId, string NetworkId), ConcurrentDictionary<string, DepositWallet>>();
        }

        public async Task<DepositWallet> ImportAsync(
            string blockchainId, 
            string networkId, 
            string address, 
            string groupName, 
            string userContext,
            string pubKey)
        {
            var importedWallet = await _blockchainWalletClient.ImportWalletAsync(blockchainId, networkId, new ImportWalletRequest
            {
                Address = address,
                PubKey = pubKey,
                TransferCallbackOptions = new TransferCallbackOptions()
            });

            if (importedWallet == null)
            {
                return null;
            }

            var depositWallet = new DepositWallet
            {
                Address = address,
                BlockchainId = blockchainId,
                GroupName = groupName,
                UserContext = userContext,
                Id = importedWallet.Id.ToString(),
                NetworkId = networkId,
                PublicKey = pubKey
            };

            await _depositWalletRepository.AddOrUpdateAsync(depositWallet);

            return depositWallet;
        }

        public async Task<IReadOnlyCollection<DepositWallet>> GetAllAsync(
            string blockchainId,
            string networkId,
            int startAfter,
            int endBefore,
            int limit
            )
        {
            var many = await _depositWalletRepository
                .GetManyAsync(blockchainId, networkId, startAfter, limit);
            
            return many;
        }


        public async Task<DepositWallet> GetByIdAsync(
            string blockchainId,
            string networkId,
            string walletId)
        {
            var depositWallet = await _depositWalletRepository.GetByIdAsync(blockchainId, networkId, walletId);

            return depositWallet;
        }
    }
}
