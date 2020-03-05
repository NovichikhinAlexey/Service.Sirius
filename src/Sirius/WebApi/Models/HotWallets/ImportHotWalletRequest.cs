namespace Sirius.WebApi.Models.HotWallets
{
    public sealed class ImportHotWalletRequest
    {
        public string GroupName { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
    }
}
