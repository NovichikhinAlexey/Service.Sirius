using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models.DepositTags;
using Sirius.WebApi.Models.Transactions;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/deposit-tags")]
    public class DepositTagsController : ControllerBase
    {
        [HttpPut("imported")]
        public async Task<ActionResult<DepositTagModel>> ImportDepositTag(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromRoute] ImportDepositTagRequest request)
        {
            return new DepositTagModel
            {
                Tag = request.Tag,
                TagType = request.TagType,
                GroupName = request.GroupName,
                UserContext = request.UserContext
            };
        }

        [HttpPost("generate")]
        public async Task<ActionResult<DepositTagModel>> GenerateDepositTag(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromRoute] GenerateDepositTagRequest request)
        {
            return new DepositTagModel
            {
                Tag = request.TagType == DestinationTagType.Text ? "generated" : "123456789",
                TagType = request.TagType,
                GroupName = request.GroupName,
                UserContext = request.UserContext
            };
        }
    }
}
