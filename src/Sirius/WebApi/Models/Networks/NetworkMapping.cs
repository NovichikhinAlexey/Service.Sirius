using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.Networks;
using Sirius.WebApi.Utilities;

namespace Sirius.WebApi.Models.Networks
{
    public static class NetworkMapping
    {
        public static NetworkModel FromDomain(IUrlHelper url, string blockchainId, Network network)
        {
            return new NetworkModel
            {
                Id = network.Id,
                Name = network.Name,
                Type = network.Type,
                Links = new NetworkLinks
                {
                    AssetsUrl = url.AssetsUrl(blockchainId, network.Id),
                    DepositWalletsUrl = url.DepositWalletsUrl(blockchainId, network.Id, null),
                    HotWalletsUrl = url.HotWalletsUrl(blockchainId, network.Id, null)
                }
            };
        }
    }
}
