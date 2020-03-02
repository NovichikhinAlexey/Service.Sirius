using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposits")]
    public class DepositsController : ControllerBase
    {
        [HttpGet]
        public void GetDeposits()
        {

        }

        [HttpGet("{id}")]
        public void GetDeposit()
        {

        }
    }
}
