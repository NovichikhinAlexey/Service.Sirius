using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions.OutgoingTransfers.Withdrawals;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/withdrawals")]
    public class WithdrawalsController : ControllerBase
    {
        [HttpPost("execute")]
        public async Task<ActionResult<WithdrawalModel>> ExecuteWithdrawal([FromRoute, FromHeader, FromBody] ExecuteWithdrawalRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<Action<Paginated<WithdrawalModel, string>>> GetWithdrawals([FromRoute, FromQuery] WithdrawalsRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WithdrawalModel>> GetWithdrawal([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
