namespace Service.Sirius.Repositories
{
    public class PostgresRepositoryConfiguration
    {
        public static string SchemaName { get; } = "sirius";

        public static string MigrationHistoryTable { get; } = "__EFMigrationsHistory_sirius";
    }
}
