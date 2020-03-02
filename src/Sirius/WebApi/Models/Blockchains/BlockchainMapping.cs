using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.Blockchains;
using Sirius.WebApi.Utilities;

namespace Sirius.WebApi.Models.Blockchains
{
    public static class BlockchainMapping
    {
        public static BlockchainModel FromDomain(IUrlHelper url, Blockchain blockchain)
        {
            return new BlockchainModel
            {
                Id = blockchain.Id, 
                Links = new BlockchainLinks {NetworksUrl = url.NetworksUrl(blockchain.Id)}
            };
        }
    }
}
