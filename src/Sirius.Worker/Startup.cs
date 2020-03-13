using System;
using System.Net.Http;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.BlockchainWalletApi.Client.Http;
using Sirius.Configuration;
using Sirius.Domain.Withdrawals;
using Sirius.Worker.HostedServices;
using Sirius.Worker.MessageConsumers;
using Swisschain.Sdk.Server.Common;

namespace Sirius.Worker
{
    public sealed class Startup : SwisschainStartup<AppConfig>
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureServicesExt(IServiceCollection services)
        {
            base.ConfigureServicesExt(services);

            //Register message consumers
            services.AddTransient<ExecuteWithdrawalConsumer>();
            services.AddTransient<DepositCompletedConsumer>();
            services.AddTransient<WithdrawalCompletedConsumer>();

            services.AddTransient<WithdrawalService>();
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
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(this.Config.RabbitMq.HostUrl, host =>
                    {
                        host.Username(this.Config.RabbitMq.Username);
                        host.Password(this.Config.RabbitMq.Password);
                    });

                    cfg.SetLoggerFactory(provider.GetRequiredService<ILoggerFactory>());

                    cfg.ReceiveEndpoint("sirius-withdrawals-execution", e =>
                    {
                        e.Consumer(provider.GetRequiredService<ExecuteWithdrawalConsumer>);
                    });

                    cfg.ReceiveEndpoint("sirius-deposits-processing", e =>
                    {
                        e.Consumer(provider.GetRequiredService<DepositCompletedConsumer>);
                    });

                    cfg.ReceiveEndpoint("sirius-withdrawals-processing", e =>
                    {
                        e.Consumer(provider.GetRequiredService<WithdrawalCompletedConsumer>);
                    });
                }));

                services.AddSingleton<IHostedService, BusService>();
            });
        }
    }
}
