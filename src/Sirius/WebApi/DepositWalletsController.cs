using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.DepositWallets;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-wallets")]
    public class DepositWalletsController : ControllerBase
    {
        [HttpPut("imported")]
        public async Task<ActionResult<DepositWalletModel>> ImportDepositWallet([FromRoute, FromBody] ImportDepositWalletRequest request)
        {
            return new DepositWalletModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Address = request.Address,
                GroupName = request.GroupName ?? "default",
                PublicKey = request.PublicKey,
                IsTrusted = false
            };
        }

        [HttpPost("generate")]
        public async Task<ActionResult<DepositWalletModel>> GenerateDepositWallet([FromRoute, FromBody] GenerateDepositWalletRequest request)
        {
            return new DepositWalletModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Address = "generated",
                GroupName = request.GroupName ?? "default",
                PublicKey = "generated",
                IsTrusted = true
            };
        }

        [HttpGet]
        public async Task<ActionResult<Paginated<DepositWalletModel, string>>> GetDepositWallets([FromRoute, FromQuery] DepositWalletsRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepositWalletModel>> GetDepositWallet([FromRoute] BlockchainNetworkEntityRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
