using Microsoft.AspNetCore.Mvc;
using Sirius.WebApi.Models.Transactions;

namespace Sirius.WebApi.Models.DepositTags
{
    public sealed class GenerateDepositTagRequest
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }

        public DestinationTagType TagType { get; set; }
    }
}
