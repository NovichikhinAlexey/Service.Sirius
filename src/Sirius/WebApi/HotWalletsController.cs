using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.HotWallets;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/hot-wallets")]
    public class HotWalletsController : ControllerBase
    {
        [HttpPut("imported")]
        public async Task<ActionResult<HotWalletModel>> ImportHotWallet([FromRoute, FromBody] ImportHotWalletRequest request)
        {
            return new HotWalletModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Address = request.Address,
                GroupName = request.GroupName ?? "default",
                PublicKey = request.PublicKey
            };
        }

        [HttpPost("{id}/designate")]
        public async Task<ActionResult> DesignateHotWallet([FromRoute, FromBody] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("designated")]
        public async Task<ActionResult<HotWalletModel>> GetActiveHotWallet([FromRoute, FromQuery] DesignatedHotWalletRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<Paginated<HotWalletModel, string>>> GetHotWallets([FromRoute, FromQuery] HotWalletsRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HotWalletModel>> GetHotWallet([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
