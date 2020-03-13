using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.BlockchainWallet.Messages.Transactions.Transfers;

namespace Sirius.Worker.MessageConsumers
{
    public class DepositCompletedConsumer : IConsumer<DepositCompleted>
    {
        private readonly ILogger<DepositCompletedConsumer> _logger;

        public DepositCompletedConsumer(ILogger<DepositCompletedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<DepositCompleted> context)
        {
            var @event = context.Message;

            _logger.LogInformation("Deposit completed {@event}", @event);
        }
    }
}
