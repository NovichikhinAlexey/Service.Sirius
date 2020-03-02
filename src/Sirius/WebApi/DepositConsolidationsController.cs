using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-consolidations")]
    public class DepositConsolidationsController : ControllerBase
    {
        [HttpGet("executed")]
        public void GetDepositConsolidations()
        {

        }

        [HttpGet("executed/{id}")]
        public void GetDepositConsolidation()
        {

        }

        [HttpGet("unsigned")]
        public void GetUnsignedDepositConsolidations()
        {

        }

        [HttpPost("unsigned/{id}/proceed")]
        public void ProceedDepositConsolidation()
        {

        }
    }
}
