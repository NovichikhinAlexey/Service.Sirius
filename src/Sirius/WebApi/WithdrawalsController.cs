using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions;
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

        [HttpGet("unsigned")]
        public async Task<ActionResult<Paginated<UnsignedTransactionModel, string>>> GetUnsignedWithdrawals([FromRoute, FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost("unsigned/{id}/proceed")]
        public async Task ProceedDepositProvision([FromRoute, FromBody] ProceedUnsignedTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
