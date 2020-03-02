using Sirius.Client.Common;
using Service.Sirius.Protos;

namespace Sirius.Client
{
    public class SiriusClient : BaseGrpcClient, ISiriusClient
    {
        public SiriusClient(string serverGrpcUrl) : base(serverGrpcUrl)
        {
            Monitoring = new Monitoring.MonitoringClient(Channel);
        }

        public Monitoring.MonitoringClient Monitoring { get; }
    }
}
