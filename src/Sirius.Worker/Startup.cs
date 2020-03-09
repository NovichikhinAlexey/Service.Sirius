using System;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sirius.Domain.Withdrawals;
using Sirius.Worker.Configuration;
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

            services.AddTransient<ExecuteWithdrawalConsumer>();
            services.AddTransient<WithdrawalService>();

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbit-liquidity.liquidity.svc.cluster.local", host =>
                    {
                        host.Username("liquidity");
                        host.Password("liquidity");
                    });

                    cfg.SetLoggerFactory(provider.GetRequiredService<ILoggerFactory>());

                    cfg.ReceiveEndpoint("sirius-withdrawals", e =>
                    {
                        e.Consumer(provider.GetRequiredService<ExecuteWithdrawalConsumer>);
                    });
                }));

                services.AddSingleton<IHostedService, BusService>();
            });
        }
    }
}
