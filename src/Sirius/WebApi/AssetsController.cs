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
        public async Task<ActionResult<Paginated<AssetModel, string>>> GetAssets(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromQuery] BlockchainNetworkEntitiesRequest request)
        {
            var blockchainAssets =_assetService.GetAssetsFor(blockchainId, networkId);
            
            if (!blockchainAssets.Any())
            {
                return NotFound();
            }

            return blockchainAssets.Select(AssetMapping.FromDomain).ToArray().Paginate(request, Url, x => x.Id);
        }

        [HttpGet("{id}", Name = nameof(GetAsset))]
        public async Task<ActionResult<AssetModel>> GetAsset(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId, 
            [FromRoute] string id)
        {
            var asset = _assetService.GetAssetForId(blockchainId, networkId, id);
            
            if (asset == default)
            {
                return NotFound();
            }

            return AssetMapping.FromDomain(asset);
        }

        [HttpGet("by-ticker/{ticker}", Name = nameof(GetAssetsByTicker))]
        public async Task<ActionResult<Paginated<AssetModel, string>>> GetAssetsByTicker(
            [FromRoute] string blockchainId,
            [FromRoute] string networkId,
            [FromRoute] AssetsByTickerRequest request)
        {
            var assets = _assetService.GetAssetsForTicker(blockchainId, networkId, request.Ticker);

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
