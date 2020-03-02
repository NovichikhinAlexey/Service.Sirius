using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions;
using Sirius.WebApi.Models.Transactions.InnerTransfers.DepositConsolidations;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-consolidations")]
    public class DepositConsolidationsController : ControllerBase
    {
        [HttpGet("executed")]
        public async Task<ActionResult<Paginated<ExecutedDepositConsolidationModel, string>>> GetExecutedDepositConsolidations([FromRoute, FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("executed/{id}")]
        public async Task<ActionResult<ExecutedDepositConsolidationModel>> GetExecutedDepositConsolidation([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("unsigned")]
        public async Task<ActionResult<Paginated<UnsignedTransactionModel, string>>> GetUnsignedDepositConsolidations([FromRoute, FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost("unsigned/{id}/proceed")]
        public void ProceedDepositConsolidation([FromRoute] ProceedUnsignedTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
