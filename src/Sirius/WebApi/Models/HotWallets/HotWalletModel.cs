namespace Sirius.WebApi.Models.HotWallets
{
    public sealed class HotWalletModel
    {
        // TODO: Should be sequential
        public string Id { get; set; }

        public string Address { get; set; }

        public string PublicKey { get; set; }

        public string GroupName { get; set; }

        public bool IsTrusted { get; set; }
    }
}
