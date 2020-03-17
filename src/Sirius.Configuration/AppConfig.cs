namespace Sirius.Configuration
{
    public class AppConfig
    {
        public BlockchainWalletApiServiceConfig BlockchainWalletApiService { get; set; }

        public RabbitMqConfig RabbitMq { get; set; }

        public DbConfig DbConfig { get; set; }
    }
}
