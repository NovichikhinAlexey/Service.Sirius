using System.Collections.Generic;
using System.Linq;

namespace Sirius.Domain.Assets
{
    public class AssetService
    {
        private readonly Dictionary<(string blockchainId, string networkId), Asset[]> _assets;

        public AssetService()
        {
            _assets = new Dictionary<(string, string), Asset[]>
            {
                [("Bitcoin", "TestNet")] = new[]
                {
                    new Asset
                    {
                        Id = "BTC",
                        Ticker = "BTC",
                        Accuracy = 8
                    }

                },
                [("Bitcoin", "RegTest")] = new[]
                {
                    new Asset
                    {
                        Id = "BTC",
                        Ticker = "BTC",
                        Accuracy = 8
                    }
                },
                [("Bitcoin", "MainNet")] = new[]
                {
                    new Asset
                    {
                        Id = "BTC",
                        Ticker = "BTC",
                        Accuracy = 8
                    }

                },
                [("Ethereum", "Ropsten")] = new[]
                {
                    new Asset
                    {
                        Id = "ETH",
                        Ticker = "ETH",
                        Accuracy = 18
                    },
                    new Asset
                    {
                        Id = "0xdac17f958d2ee523a2206206994597c13d831ec7",
                        Ticker = "USDT",
                        Address = "0xdac17f958d2ee523a2206206994597c13d831ec7"
                    },
                    new Asset
                    {
                        Id = "0x5ac17f958d2ee523a2206206994597c13d831ec8",
                        Ticker = "USDT",
                        Address = "0x5ac17f958d2ee523a2206206994597c13d831ec8"
                    }
                },
                [("Ethereum", "MainNet")] = new[]
                {
                    new Asset
                    {
                        Id = "ETH",
                        Ticker = "ETH",
                        Accuracy = 18
                    },
                    new Asset
                    {
                        Id = "0xdac17f958d2ee523a2206206994597c13d831ec7",
                        Ticker = "USDT",
                        Address = "0xdac17f958d2ee523a2206206994597c13d831ec7"
                    }
                }
            };
        }

        public IReadOnlyDictionary<(string blockchainId, string networkId), Asset[]> GetAllAssets()
        {
            return _assets;
        }

        public IReadOnlyCollection<Asset> GetAssetsFor(string blockchainId, string networkId)
        {
            _assets.TryGetValue((blockchainId, networkId), out var assets);

            return assets ?? new Asset[0];
        }

        public Asset GetAssetForId(string blockchainId, string networkId, string assetId)
        {
            _assets.TryGetValue((blockchainId, networkId), out var assets);

            var result = assets?.SingleOrDefault(x => x.Id == assetId);

            return result;
        }

        public IReadOnlyCollection<Asset> GetAssetsForTicker(string blockchainId, string networkId, string ticker)
        {
            _assets.TryGetValue((blockchainId, networkId), out var assets);

            var result = assets?.Where(x => x.Ticker == ticker).ToArray();

            return result ?? new Asset[0];
        }
    }
}
