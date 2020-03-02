using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/hot-wallet-provisions")]
    public class HotWalletProvisionsController : ControllerBase
    {
        [HttpGet]
        public void GetHotWalletProvisions()
        {

        }

        [HttpGet("{id}")]
        public void GetHotWalletProvision()
        {

        }
    }
}
