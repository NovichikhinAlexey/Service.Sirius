using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Service.Sirius.Repositories.DbContexts;
using Sirius.Domain.Repositories;

namespace Service.Sirius.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddTransient<IDepositWalletRepository, DepositWalletRepository>();
            serviceCollection.AddTransient<IHotWalletRepository, HotWalletRepository>();
            serviceCollection.AddTransient<IDepositsRepository, DepositRepository>();
            serviceCollection.AddTransient<IWithdrawalRepository, WithdrawalRepository>();
            
            serviceCollection.AddSingleton<DbContextOptionsBuilder<SiriusContext>>(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<SiriusContext>();
                optionsBuilder.UseNpgsql(connectionString,
                    builder =>
                        builder.MigrationsHistoryTable(
                            PostgresRepositoryConfiguration.MigrationHistoryTable,
                            PostgresRepositoryConfiguration.SchemaName));

                return optionsBuilder;
            });
            
            return serviceCollection;
        }

        public static IServiceCollection Migrate(this IServiceCollection serviceCollection)
        {
            var contextOptions = serviceCollection.BuildServiceProvider().GetRequiredService<DbContextOptionsBuilder<SiriusContext>>();

            using (var context = new SiriusContext(contextOptions.Options))
            {
                context.Database.Migrate();
            }

            return serviceCollection;
        }
    }
}
