using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models.DepositTags;
using Sirius.WebApi.Models.Transactions;

namespace Sirius.WebApi
{
    // TODO: Add this to the Blockchain Wallet too
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-tags")]
    public class DepositTagsController : ControllerBase
    {
        [HttpPost("generate")]
        public async Task<ActionResult<DepositTagModel>> GenerateDepositTag([FromRoute] GenerateDepositTagRequest request)
        {
            return new DepositTagModel
            {
                Tag = request.TagType == DestinationTagType.Text ? "generated" : "123456789"
            };
        }
    }
}
