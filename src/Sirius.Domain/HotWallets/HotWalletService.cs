using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.BlockchainWalletApi.Client.Http;

namespace Sirius.Domain.HotWallets
{
    public class HotWalletService
    {
        private readonly ConcurrentDictionary<(string BlockchainId, string NetworkId, string GroupName), HotWallet> _designatedWallets;
        private readonly ConcurrentDictionary<(string BlockchainId, string NetworkId), ConcurrentDictionary<string, HotWallet>> _wallets;
        private readonly IBlockchainWalletClient _blockchainWalletClient;

        public HotWalletService(IBlockchainWalletClient blockchainWalletClient)
        {
            _blockchainWalletClient = blockchainWalletClient;
            _wallets = new ConcurrentDictionary<(string BlockchainId, string NetworkId), ConcurrentDictionary<string, HotWallet>>();
            _designatedWallets = new ConcurrentDictionary<(string BlockchainId, string NetworkId, string GroupName), HotWallet>();
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

            _designatedWallets[(blockchainId, networkId, groupName)] = hotwallet;

            return hotwallet;
        }

        public Task<HotWallet> GetDesignatedWalletAsync(
            string blockchainId,
            string networkId,
            string groupName)
        {
            if (!_designatedWallets.TryGetValue((blockchainId, networkId, groupName), out var hotwallet))
                return Task.FromResult<HotWallet>(null);

            return Task.FromResult(hotwallet);
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

            _wallets.AddOrUpdate(
                (blockchainId, networkId),
                addValueFactory: key =>
                {
                    return new ConcurrentDictionary<string, HotWallet> { [hotWallet.Id] = hotWallet};
                },
                updateValueFactory: (key, current) =>
                {
                    var existing = current.Values
                        .FirstOrDefault(x => x.Address.Equals(address, StringComparison.InvariantCultureIgnoreCase));

                    if (existing == null)
                    {
                        current.TryAdd(hotWallet.Id, hotWallet);
                    }
                    else
                    {
                        current[hotWallet.Id] = hotWallet;
                    }

                    return current;
                });

            return hotWallet;
        }

        public Task<IReadOnlyCollection<HotWallet>> GetAllAsync(
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

            return Task.FromResult((IReadOnlyCollection<HotWallet>)result);
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
            _wallets.TryGetValue((blockchainId, networkId), out var dictionary);

            if (dictionary != null &&
                dictionary.TryGetValue(walletId, out var wallet))
            {
                return Task.FromResult(wallet);
            }

            return Task.FromResult<HotWallet>(null);
        }
    }
}
