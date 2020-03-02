namespace Sirius.WebApi.Models.Assets
{
    public sealed class AssetModel
    {
        public string Id { get; set; }
        public string Ticker { get; set; }
        public string Address { get; set; }
        public int Accuracy { get;set; }
    }
}
