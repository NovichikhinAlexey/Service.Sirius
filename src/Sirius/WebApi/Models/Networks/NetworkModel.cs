using Newtonsoft.Json;
using Sirius.Domain.Networks;

namespace Sirius.WebApi.Models.Networks
{
    public sealed class NetworkModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public NetworkType Type { get; set; }

        [JsonProperty("_links")]
        public NetworkLinks Links { get; set; }
    }
}
