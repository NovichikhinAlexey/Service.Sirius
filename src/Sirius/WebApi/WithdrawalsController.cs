using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/withdrawals")]
    public class WithdrawalsController : ControllerBase
    {
        [HttpPost("execute")]
        public void ExecuteWithdrawal()
        {

        }

        [HttpGet]
        public void GetWithdrawals()
        {

        }

        [HttpGet("{id}")]
        public void GetWithdrawal()
        {

        }
    }
}
