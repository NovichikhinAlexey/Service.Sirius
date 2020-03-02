using System.Security.Policy;
using Newtonsoft.Json;

namespace Sirius.WebApi.Models.Blockchains
{
    public sealed class BlockchainModel
    {
        public string Id { get; set; }

        [JsonProperty("_links")]
        public BlockchainLinks Links { get; set; }
    }
}
