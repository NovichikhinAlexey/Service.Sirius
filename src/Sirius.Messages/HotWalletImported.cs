namespace Sirius.Messages
{
    public class HotWalletImported
    {
        public string BlockchainId { get; set; }
        public string NetworkId { get; set; }
        public string Address { get; set; }
        public string GroupName { get; set; }
        public string PublicKey { get; set; }
    }
}
