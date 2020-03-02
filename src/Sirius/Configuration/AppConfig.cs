namespace Sirius.Configuration
{
    public class AppConfig
    {
        public ApiService ApiService { get; set; }
    }

    public class ApiService
    {
        public string GasAmountReservePercentage { get; set; }
    }
}
