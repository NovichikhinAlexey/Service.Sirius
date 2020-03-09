using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.HotWallets;
using Sirius.Domain.Withdrawals;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions.OutgoingTransfers.Withdrawals;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/withdrawals")]
    public class WithdrawalsController : ControllerBase
    {
        private readonly HotWalletService _hotWalletService;
        private readonly ISendEndpointProvider _commandsSender;

        public WithdrawalsController(
            HotWalletService hotWalletService,
            ISendEndpointProvider commandsSender)
        {
            _hotWalletService = hotWalletService;
            _commandsSender = commandsSender;
        }

        [HttpPost("execute")]
        public async Task<ActionResult<WithdrawalModel>> ExecuteWithdrawal(
            [FromHeader(Name = "X-Request-ID"), Required] Guid requestId,
            [FromRoute(Name = "blockchainId")] string blockchainId,
            [FromRoute(Name = "networkId")] string networkId,
            [FromBody] ExecuteWithdrawalRequest request)
        {
            var hotWallet = await _hotWalletService.GetByIdAsync(blockchainId, networkId, request.HotWalletId);

            if (hotWallet == null)
                return NotFound();

            await _commandsSender.Send(new ExecuteWithdrawal
            {
                RequestId = requestId,
                BlockchainId = blockchainId,
                NetworkId = networkId,
                HotWalletAddress = hotWallet.Address,
                DestinationAddress = request.DestinationAddress,
                AssetId = request.AssetId,
                Amount = request.Amount
            });

            return Ok(new WithdrawalModel
            {
                //GroupName = ,
                //Id = ,
                //AcceptedDateTime = ,
                //Amount = ,
                //AssetId = ,
                //BatchId = ,
                //DestinationAddress = ,
                //DestinationTag = ,
                //WithdrawalFees = ,
                //TransactionHash = ,
                //ErrorMessage = ,
                //HotWalletId = ,
                //FeePayer = ,
                //State = ,
                //TransactionFees = ,
            });
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
