using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.BlockchainWallet.Messages.Transactions.Transfers;

namespace Sirius.Worker.MessageConsumers
{
    public class WithdrawalCompletedConsumer : IConsumer<WithdrawalCompleted>
    {
        private readonly ILogger<WithdrawalCompletedConsumer> _logger;

        public WithdrawalCompletedConsumer(ILogger<WithdrawalCompletedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<WithdrawalCompleted> context)
        {
            var @event = context.Message;

            _logger.LogInformation("Withdrawal completed {@event}", @event);
        }
    }
}
