using System;
using System.Net.Http;
using MassTransit;
using Sirius.Configuration;
using Sirius.GrpcServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.BlockchainWalletApi.Client.Http;
using Sirius.Domain.Assets;
using Sirius.Domain.DepositWallets;
using Sirius.Domain.HotWallets;
using Sirius.Domain.Networks;
using Sirius.HostedServices;
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

            services.AddMassTransit(x =>
            {
                EndpointConvention.Map<ExecuteWithdrawal>(new Uri("queue:sirius-withdrawals"));

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbit-liquidity.liquidity.svc.cluster.local", host =>
                    {
                        host.Username("liquidity");
                        host.Password("liquidity");
                    });

                    cfg.SetLoggerFactory(provider.GetRequiredService<ILoggerFactory>());
                }));

                services.AddSingleton<IHostedService, BusService>();
            });
        }

        protected override void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            base.RegisterEndpoints(endpoints);

            endpoints.MapGrpcService<MonitoringService>();
        }
    }
}
