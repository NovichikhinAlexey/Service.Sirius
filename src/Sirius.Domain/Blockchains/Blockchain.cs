namespace Sirius.Domain.Blockchains
{
    public sealed class Blockchain
    {
        public string Id { get; set; }
        public DepositSegregationType DepositSegregationType { get; set; }
        public BlockchainCapabilities Capabilities { get; set; }
    }
}
