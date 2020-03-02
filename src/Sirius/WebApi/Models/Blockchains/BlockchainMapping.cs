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
                DepositSegregationType = blockchain.DepositSegregationType,
                Capabilities = blockchain.Capabilities,
                Requirements = blockchain.Requirements,
                Links = new BlockchainLinks {NetworksUrl = url.NetworksUrl(blockchain.Id)}
            };
        }
    }
}
