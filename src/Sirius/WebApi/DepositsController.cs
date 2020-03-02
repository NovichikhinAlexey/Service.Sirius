using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions.IncomingTransfers.Deposits;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposits")]
    public class DepositsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Paginated<DepositModel, string>>> GetDeposits([FromRoute, FromQuery] DepositsRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepositModel>> GetDeposit([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
