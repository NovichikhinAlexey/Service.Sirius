using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions.OutgoingTransfers.WithdrawalBatches;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/withdrawal-batches")]
    public class WithdrawalBatchesController : ControllerBase
    {
        [HttpGet("executed")]
        public async Task<ActionResult<Paginated<ExecutedWithdrawalBatchModel, string>>> GetExecutedWithdrawalBatches([FromRoute, FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("executed/{id}")]
        public async Task<ActionResult<ExecutedWithdrawalBatchModel>> GetExecutedWithdrawalBatch([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
