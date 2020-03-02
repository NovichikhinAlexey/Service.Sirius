using Newtonsoft.Json;
using Sirius.Domain.Blockchains;

namespace Sirius.WebApi.Models.Blockchains
{
    public sealed class BlockchainModel
    {
        public string Id { get; set; }
        public DepositSegregationType DepositSegregationType { get; set; }
        public BlockchainCapabilities Capabilities { get; set; }
        public BlockchainRequirements Requirements { get; set; }

        [JsonProperty("_links")]
        public BlockchainLinks Links { get; set; }
    }
}
