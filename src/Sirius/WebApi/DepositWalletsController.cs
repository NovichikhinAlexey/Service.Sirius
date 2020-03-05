using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.Deposits;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.DepositWallets;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-wallets")]
    public class DepositWalletsController : ControllerBase
    {
        private readonly DepositWalletService _depositWalletService;

        public DepositWalletsController(DepositWalletService depositWalletService)
        {
            _depositWalletService = depositWalletService;
        }

        [HttpPut("imported")]
        public async Task<ActionResult<DepositWalletModel>> ImportDepositWallet(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromBody] ImportDepositWalletRequest request)
        {
            var wallet = await _depositWalletService.ImportAsync(
                blockchainId,
                networkId,
                request.Address,
                request.GroupName,
                request.PublicKey);

            return Ok(DepositWalletModelMapper.MapFromDomain(wallet));
        }

        [HttpGet]
        public async Task<ActionResult<Paginated<DepositWalletModel, string>>> GetDepositWallets(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId, 
            [FromQuery] DepositWalletsRequest request)
        {
            int.TryParse(request.StartingAfter, out var startingAfter);
            int.TryParse(request.EndingBefore, out var endingBefore);

            var wallets = await _depositWalletService.GetAllAsync(
                blockchainId,
                networkId,
                startingAfter,
                endingBefore,
                request.Limit);

            if (wallets == null || !wallets.Any())
                return NotFound();

            return Ok(wallets
                .Select(DepositWalletModelMapper.MapFromDomain)
                .ToArray()
                .Paginate(request, Url, model => model.Id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepositWalletModel>> GetDepositWallet(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId, 
            [FromRoute] string id)
        {
            var wallet = await _depositWalletService.GetByIdAsync(blockchainId, networkId, id);

            if (wallet == null)
                return NotFound();


            return Ok(DepositWalletModelMapper.MapFromDomain(wallet));
        }
    }
}
