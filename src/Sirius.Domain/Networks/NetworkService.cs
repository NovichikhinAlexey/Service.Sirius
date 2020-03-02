﻿using System.Collections.Generic;
using System.Linq;

namespace Sirius.Domain.Networks
{
    public class NetworkService
    {
        private readonly Dictionary<string, Network[]> _networks;

        public NetworkService()
        {
            //TODO: Init from settings
            _networks = new Dictionary<string, Network[]>
            {
                ["Bitcoin"] =
                    new[]
                    {
                        new Network {Id = "MainNet", Name = "Main net", Type = NetworkType.Public},
                        new Network {Id = "TestNet", Name = "Test net", Type = NetworkType.Test},
                        new Network {Id = "RegTest", Name = "Swisschain RegTest", Type = NetworkType.Private,},
                        new Network {Id = "Warehouse1", Name = "Warehouse USA", Type = NetworkType.Private},
                        new Network {Id = "Warehouse2", Name = "Warehouse Europe", Type = NetworkType.Private}
                    },
                ["Ethereum"] = new[]
                {
                    new Network {Id = "MainNet", Name = "Main net", Type = NetworkType.Public},
                    new Network {Id = "Ropsten", Name = "Ropesten test net", Type = NetworkType.Test,},
                    new Network {Id = "Kovan", Name = "Kovan test net", Type = NetworkType.Test},
                }
            };
        }

        public IReadOnlyDictionary<string, Network[]> GetAllNetworks()
        {
            return _networks;
        }

        public IReadOnlyCollection<Network> GetNetworksByBlockchainType(string blockchainType)
        {
            _networks.TryGetValue(blockchainType, out var networksByBlockchain);

            return networksByBlockchain;
        }

        public Network GetNetworkById(string networkId)
        {
            var result = _networks.Values
                .SelectMany(x => x).FirstOrDefault(x => x.Id.Equals(networkId));

            return result;
        }
    }
}
