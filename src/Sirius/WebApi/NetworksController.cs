using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.Networks;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Networks;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks")]
    public sealed class NetworksController : ControllerBase
    {
        private readonly NetworkService _networkService;

        public NetworksController(NetworkService networkService)
        {
            _networkService = networkService;
        }

        [HttpGet(Name = nameof(GetNetworks))]
        public async Task<ActionResult<Paginated<NetworkModel, string>>> GetNetworks([FromRoute, FromQuery] NetworksRequest request)
        {
            //this.Request.RouteValues
            var networks = _networkService.GetAllNetworks();
            if (!networks.TryGetValue(request.BlockchainId, out var blockchainNetworks))
            {
                return NotFound();
            }

            return blockchainNetworks
                .Select(x => NetworkMapping.FromDomain(Url, request.BlockchainId, x))
                .ToArray()
                .Paginate(request, Url, x => x.Id);
        }
    }
}
