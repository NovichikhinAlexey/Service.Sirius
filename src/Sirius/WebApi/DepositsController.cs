using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.Deposits;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Transactions.IncomingTransfers;
using Sirius.WebApi.Models.Transactions.IncomingTransfers.Deposits;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposits")]
    public class DepositsController : ControllerBase
    {
        private readonly DepositService _depositService;

        public DepositsController(DepositService depositService)
        {
            _depositService = depositService;
        }
        [HttpGet]
        public async Task<ActionResult<Paginated<DepositModel, string>>> GetDeposits(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromQuery] DepositsRequest request)
        {
            int.TryParse(request.StartingAfter, out var startingAfter);
            var many = await _depositService.GetManyAsync(blockchainId, networkId, startingAfter, request.Limit);

            return many.Select(MapToDepositModel)
                .ToArray()
                .Paginate(request, Url, model => model.Id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepositModel>> GetDeposit(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromRoute] string id)
        {
            var deposit = await _depositService.GetByIdAsync(blockchainId, networkId, id);

            return Ok(MapToDepositModel(deposit));
        }

        private static DepositModel MapToDepositModel(Deposit deposit)
        {
            return new DepositModel()
            {
                Id = deposit.Id,
                AssetId = deposit.AssetId,
                DepositWalletId = deposit.WalletId,
                HotWalletId = deposit.WalletId,
                TransactionHash = deposit.Id,
                Sources = deposit.Sources.Select(x => new IncomingTransferSource()
                {
                    Address = x.Address,
                    Amount = x.Amount
                }).ToArray(),
            };
        }
    }
}
