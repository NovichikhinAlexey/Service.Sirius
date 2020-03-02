using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/withdrawal-aggregations")]
    public class WithdrawalAggregationsController : ControllerBase
    {
        [HttpGet("executed")]
        public void GetWithdrawals()
        {

        }

        [HttpGet("executed/{id}")]
        public void GetWithdrawal()
        {

        }

        [HttpGet("unsigned")]
        public void GetUnsignedWithdrawals()
        {

        }

        [HttpPost("unsigned/{id}/proceed")]
        public void ProceedWithdrawal()
        {

        }
    }
}
