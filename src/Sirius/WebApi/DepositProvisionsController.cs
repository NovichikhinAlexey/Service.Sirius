using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-provisions")]
    public class DepositProvisionsController : ControllerBase
    {
        [HttpGet("executed")]
        public void GetDepositProvisions()
        {

        }

        [HttpGet("executed/{id}")]
        public void GetDepositProvision()
        {

        }

        [HttpGet("unsigned")]
        public void GetUnsignedDepositProvisions()
        {

        }

        [HttpPost("unsigned/{id}/proceed")]
        public void ProceedDepositProvision()
        {

        }
    }
}
