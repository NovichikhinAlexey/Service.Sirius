using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Service.Sirius.Repositories.DbContexts;

namespace Service.Sirius.Repositories.DesignTime
{
    public class ContextFactory : IDesignTimeDbContextFactory<SiriusContext>
    {
        public SiriusContext CreateDbContext(string[] args)
        {
            var connString = Environment.GetEnvironmentVariable("POSTGRE_SQL_CONNECTION_STRING");

            var optionsBuilder = new DbContextOptionsBuilder<SiriusContext>();
            optionsBuilder.UseNpgsql(connString);

            return new SiriusContext(optionsBuilder.Options);
        }
    }
}
