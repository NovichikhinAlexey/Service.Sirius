using Sirius.Domain.Assets;

namespace Sirius.WebApi.Models.Assets
{
    public static class AssetMapping
    {
        public static AssetModel FromDomain(Asset asset)
        {
            return new AssetModel
            {
                Id = asset.Id, 
                Ticker = asset.Ticker, 
                Address = asset.Address, 
                Accuracy = asset.Accuracy
            };
        }
    }
}
