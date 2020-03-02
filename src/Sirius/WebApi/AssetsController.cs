using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain.Assets;
using Sirius.WebApi.Models;
using Sirius.WebApi.Models.Assets;

namespace Sirius.WebApi
{
    [ApiController]
    [Route("api/blockchains/{blockchainId}/networks/{networkId}/assets")]
    public sealed class AssetsController : ControllerBase
    {
        private readonly AssetService _assetService;

        public AssetsController(AssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet(Name = nameof(GetAssets))]
        public async Task<ActionResult<Paginated<AssetModel, string>>> GetAssets([FromRoute, FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            var blockchainAssets =_assetService.GetAssetsFor(request.BlockchainId, request.NetworkId);
            
            if (!blockchainAssets.Any())
            {
                return NotFound();
            }

            return blockchainAssets.Select(AssetMapping.FromDomain).ToArray().Paginate(request, Url, x => x.Id);
        }

        [HttpGet("{id}", Name = nameof(GetAsset))]
        public async Task<ActionResult<AssetModel>> GetAsset([FromRoute] BlockchainNetworkEntityRequest request)
        {
            var asset = _assetService.GetAssetForId(request.BlockchainId, request.NetworkId, request.Id);
            
            if (asset == default)
            {
                return NotFound();
            }

            return AssetMapping.FromDomain(asset);
        }

        [HttpGet("by-ticker/{ticker}", Name = nameof(GetAssetsByTicker))]
        public async Task<ActionResult<Paginated<AssetModel, string>>> GetAssetsByTicker([FromRoute] AssetsByTickerRequest request)
        {
            var assets = _assetService.GetAssetsForTicker(request.BlockchainId, request.NetworkId, request.Ticker);

            if (!assets.Any())
            {
                return NotFound();
            }

            return assets.Select(AssetMapping.FromDomain).ToArray().Paginate(request, Url, x => x.Id);
        }

        [HttpGet("by-address/{address}", Name = nameof(GetAssetByAddress))]
        public async Task<ActionResult<AssetModel>> GetAssetByAddress([FromRoute] AssetsByAddressRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
