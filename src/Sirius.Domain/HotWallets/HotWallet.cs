namespace Sirius.Domain.HotWallets
{
    public class HotWallet
    {
        public string Id { get; set; }
        public string BlockchainId { get; set; }

        public string NetworkId { get; set; }

        public string Address { get; set; }

        public string PublicKey { get; set; }

        public string GroupName { get; set; }
    }
}
