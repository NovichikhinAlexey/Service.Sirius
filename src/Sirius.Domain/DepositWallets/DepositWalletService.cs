using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.BlockchainWallets.Http.Client;

namespace Sirius.Domain.DepositWallets
{
    public class DepositWalletService
    {
        private readonly ConcurrentDictionary<(string BlockchainId, string NetworkId), ConcurrentDictionary<string, DepositWallet>> _wallets;
        private readonly IBlockchainWalletClient _blockchainWalletClient;

        public DepositWalletService(IBlockchainWalletClient blockchainWalletClient)
        {
            _blockchainWalletClient = blockchainWalletClient;
            _wallets = new ConcurrentDictionary<(string BlockchainId, string NetworkId), ConcurrentDictionary<string, DepositWallet>>();
        }

        public async Task<DepositWallet> ImportAsync(
            string blockchainId, 
            string networkId, 
            string address, 
            string groupName, 
            string pubKey = null)
        {
            var importedWallet = await _blockchainWalletClient.ImportWalletAsync(blockchainId, networkId, new ImportWalletRequest()
            {
                Address = address,
                TransferCallbackOptions = new TransferCallbackOptions()
            });

            if (importedWallet == null)
            {
                return null;
            }

            var depositWallet = new DepositWallet()
            {
                Address = address,
                BlockchainId = blockchainId,
                GroupName = groupName,
                Id = importedWallet.Id.ToString(),
                NetworkId = networkId,
                PublicKey = pubKey
            };

            _wallets.AddOrUpdate(
                (blockchainId, networkId),
                addValueFactory: key =>
                {
                    return new ConcurrentDictionary<string, DepositWallet> { [depositWallet.Id] = depositWallet};
                },
                updateValueFactory: (key, current) =>
                {
                    var existing = current.Values
                        .FirstOrDefault(x =>
                            x.Address.Equals(depositWallet.Address, StringComparison.InvariantCultureIgnoreCase));

                    if (existing == null)
                    {
                        current.TryAdd(depositWallet.Id, depositWallet);
                    }
                    else
                    {
                        depositWallet = existing;
                    }

                    return current;
                });

            return depositWallet;
        }

        public Task<IReadOnlyCollection<DepositWallet>> GetAllAsync(
            string blockchainId,
            string networkId,
            int startAfter,
            int endBefore,
            int limit
            )
        {
            _wallets.TryGetValue((blockchainId, networkId), out var dictionary);
            
            var result = dictionary
                ?.Values
                .Skip(startAfter)
                .Take(limit)
                .ToArray();

            return Task.FromResult((IReadOnlyCollection<DepositWallet>)result);
        }


        public Task<DepositWallet> GetByIdAsync(
            string blockchainId,
            string networkId,
            string walletId)
        {
            _wallets.TryGetValue((blockchainId, networkId), out var dictionary);

            if (dictionary != null &&
                dictionary.TryGetValue(walletId, out var wallet))
            {
                return Task.FromResult(wallet);
            }

            return Task.FromResult<DepositWallet>(null);
        }
    }
}
