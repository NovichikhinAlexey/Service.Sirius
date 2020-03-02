using Service.Sirius.Protos;

namespace Sirius.Client
{
    public interface ISiriusClient
    {
        Monitoring.MonitoringClient Monitoring { get; }
    }
}
