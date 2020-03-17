using System;
using System.Net.Http;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.BlockchainWalletApi.Client.Http;
using Service.Sirius.Repositories.Extensions;
using Sirius.Configuration;
using Sirius.Domain.Assets;
using Sirius.Domain.Deposits;
using Sirius.Domain.DepositWallets;
using Sirius.Domain.HotWallets;
using Sirius.Domain.Networks;
using Sirius.Domain.Withdrawals;
using Sirius.GrpcServices;
using Sirius.HostedServices;
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

            services.RegisterRepository(this.Config.DbConfig.ConnectionString);
            services.Migrate();

            services.AddSingleton<NetworkService>();
            services.AddSingleton<AssetService>();
            services.AddTransient<WithdrawalService>();
            services.AddTransient<DepositService>();
            services.AddTransient<HotWalletService>();
            services.AddTransient<DepositWalletService>();
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
                EndpointConvention.Map<ExecuteWithdrawal>(new Uri("queue:sirius-withdrawals-execution"));

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(this.Config.RabbitMq.HostUrl, host =>
                    {
                        host.Username(this.Config.RabbitMq.Username);
                        host.Password(this.Config.RabbitMq.Password);
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
