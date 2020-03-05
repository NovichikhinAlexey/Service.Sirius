namespace Sirius.Configuration
{
    public class AppConfig
    {
        public BlockchainWalletApiService BlockchainWalletApiService { get; set; }
    }

    public class BlockchainWalletApiService
    {
        public string Url { get; set; }
    }
}
