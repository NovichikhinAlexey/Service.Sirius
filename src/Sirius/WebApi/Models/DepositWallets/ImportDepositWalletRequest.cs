namespace Sirius.WebApi.Models.DepositWallets
{
    public sealed class ImportDepositWalletRequest
    {
        public string GroupName { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public string UserContext { get; set; }
    }
}
