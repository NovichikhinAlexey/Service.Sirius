using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
        private readonly WithdrawalService _withdrawalService;
        private readonly HotWalletService _hotWalletService;

        public WithdrawalsController(
            WithdrawalService withdrawalService,
            HotWalletService hotWalletService)
        {
            _withdrawalService = withdrawalService;
            _hotWalletService = hotWalletService;
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

            await _withdrawalService.ExecuteTransferAsync(
                requestId, 
                blockchainId, 
                networkId, 
                hotWallet.Address, 
                request.DestinationAddress,
                request.AssetId,
                request.Amount);

            return Ok(new WithdrawalModel()
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
