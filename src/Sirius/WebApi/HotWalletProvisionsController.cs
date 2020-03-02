using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions.IncomingTransfers.HotWalletProvisions;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/hot-wallet-provisions")]
    public class HotWalletProvisionsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Paginated<HotWalletProvisionModel, string>>> GetHotWalletProvisions([FromRoute, FromQuery] HotWalletProvisionRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotWalletProvisionModel>> GetHotWalletProvision([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
