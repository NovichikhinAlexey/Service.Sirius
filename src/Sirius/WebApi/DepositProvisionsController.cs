﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions;
using Sirius.WebApi.Models.Transactions.InnerTransfers.DepositProvisions;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-provisions")]
    public class DepositProvisionsController : ControllerBase
    {
        [HttpGet("executed")]
        public async Task<ActionResult<Paginated<ExecutedDepositProvisionModel, string>>> GetExecutedDepositProvisions([FromRoute, FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("executed/{id}")]
        public async Task<ActionResult<ExecutedDepositProvisionModel>> GetExecutedDepositProvision([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("unsigned")]
        public async Task<ActionResult<Paginated<UnsignedTransactionModel, string>>> GetUnsignedDepositProvisions([FromRoute, FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost("unsigned/{id}/proceed")]
        public void ProceedDepositProvision([FromRoute] ProceedUnsignedTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
