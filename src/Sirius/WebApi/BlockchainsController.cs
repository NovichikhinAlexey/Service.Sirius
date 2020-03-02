using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.Blockchains;
using Sirius.WebApi.Models.Blockchains;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains")]
    public sealed class BlockchainsController : ControllerBase
    {
        [HttpGet(Name = nameof(GetBlockchains))]
        public async Task<ActionResult<BlockchainModel[]>> GetBlockchains()
        {
            var blockchains = new[]
            {
                new Blockchain {Id = "Bitcoin", DepositSegregationType = DepositSegregationType.ByWallets},
                new Blockchain {Id = "Ethereum", DepositSegregationType = DepositSegregationType.ByWallets},
                new Blockchain
                {
                    Id = "Stellar",
                    DepositSegregationType = DepositSegregationType.ByTags,
                    Capabilities = new BlockchainCapabilities
                    {
                        DepositTags = new DepositTagsCapabilities
                        {
                            Number = true,
                            Text = true,
                            MinNumber = 0,
                            MaxNumber = BigInteger.Parse("18446744073709551615"),
                            MaxTextLength = 28,
                            NumberTagNames = new Dictionary<string, string> {["En-en"] = "Memo ID"},
                            TextTagNames = new Dictionary<string, string> {["En-en"] = "Memo text"}
                        }
                    }
                }
            };

            return blockchains.Select(x => BlockchainMapping.FromDomain(Url, x)).ToArray();
        }
    }
}
