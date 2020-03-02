using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-tags")]
    public class DepositTagsController : ControllerBase
    {
        [HttpPost("generate")]
        public void GenerateDepositTag()
        {

        }
    }
}
