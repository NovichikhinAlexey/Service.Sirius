using System.Net.Http;
using Sirius.Configuration;
using Sirius.GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.BlockchainWalletApi.Client.Http;
using Sirius.Domain.Assets;
using Sirius.Domain.DepositWallets;
using Sirius.Domain.HotWallets;
using Sirius.Domain.Networks;
using Sirius.Domain.Withdrawals;
using Swisschain.Sdk.Server.Common;

namespace Sirius
{
    public sealed class Startup : SwisschainStartup<AppConfig>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureServicesExt(IServiceCollection services)
        {
            base.ConfigureServicesExt(services);

            services.AddSingleton<NetworkService>();
            services.AddSingleton<AssetService>();
            services.AddSingleton<DepositWalletService>();
            services.AddSingleton<HotWalletService>();
            services.AddSingleton<WithdrawalService>();
            services.AddHttpClient();

            services.AddTransient<IBlockchainWalletClient>(x =>
            {
                var clientFactory = x.GetRequiredService<IHttpClientFactory>();
                var client = clientFactory.CreateClient();
                var baseUrl = this.Config.BlockchainWalletApiService.Url;

                return new BlockchainWalletClient(baseUrl, client);
            });
        }

        protected override void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            base.RegisterEndpoints(endpoints);

            endpoints.MapGrpcService<MonitoringService>();
        }
    }
}
