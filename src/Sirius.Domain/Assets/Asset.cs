namespace Sirius.Domain.Assets
{
    public sealed class Asset
    {
        // TODO: Use guid for ID?
        public string Id { get; set; }
        public string Ticker { get; set; }
        public string Address { get; set; }
        public int Accuracy { get;set; }
    }
}
