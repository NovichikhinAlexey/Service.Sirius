using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.HotWallets;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.HotWallets;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/hot-wallets")]
    public class HotWalletsController : ControllerBase
    {
        private readonly HotWalletService _hotWalletService;

        public HotWalletsController(HotWalletService hotWalletService)
        {
            _hotWalletService = hotWalletService;
        }

        [HttpPut("imported")]
        public async Task<ActionResult<HotWalletModel>> ImportHotWallet(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromBody] ImportHotWalletRequest request)
        {
            var importWallet = await _hotWalletService.ImportAsync(
                blockchainId, 
                networkId, 
                request.Address,
                request.GroupName,
                request.PublicKey);

            return Ok(HotWalletModelMapper.MapFromDomain(importWallet));
        }

        [HttpPost("generate")]
        public async Task<ActionResult<HotWalletModel>> GenerateHotWallet(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromBody] GenerateHotWalletRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{id}/{groupName}/designate")]
        public async Task<ActionResult> DesignateHotWallet(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromRoute] string groupName,
            [FromRoute] string id)
        {
            var hotwallet = await _hotWalletService.DesignateWalletAsync(blockchainId, networkId, groupName, id);

            if (hotwallet == null)
                return NotFound();

            return Ok();
        }

        [HttpGet("{groupName}/designated")]
        public async Task<ActionResult<HotWalletModel>> GetActiveHotWallet(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromRoute] string groupName)
        {
            var hotwallet = await _hotWalletService.GetDesignatedWalletAsync(blockchainId, networkId, groupName);

            if (hotwallet == null)
                return NotFound();

            return Ok(HotWalletModelMapper.MapFromDomain(hotwallet));
        }

        [HttpGet]
        public async Task<ActionResult<Paginated<HotWalletModel, string>>> GetHotWallets(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromQuery] HotWalletsRequest request)
        {
            int.TryParse(request.StartingAfter, out var startingAfter);
            int.TryParse(request.EndingBefore, out var endingBefore);

            var wallets = await _hotWalletService.GetAllAsync(
                blockchainId,
                networkId,
                startingAfter,
                endingBefore,
                request.Limit);

            if (wallets == null || !wallets.Any())
                return NotFound();

            return Ok(wallets
                .Select(HotWalletModelMapper.MapFromDomain)
                .ToArray()
                .Paginate(request, Url, model => model.Id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotWalletModel>> GetHotWallet(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId, 
            [FromRoute] string id)
        {
            var wallet = await _hotWalletService.GetByIdAsync(blockchainId, networkId, id);

            if (wallet == null)
                return NotFound();


            return Ok(HotWalletModelMapper.MapFromDomain(wallet));
        }
    }
}
