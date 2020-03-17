using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Service.BlockchainWallet.Messages.Transactions.Transfers;
using Sirius.Domain.Deposits;
using Sirius.Domain.DepositWallets;
using Sirius.Domain.HotWallets;

namespace Sirius.Worker.MessageConsumers
{
    public class DepositCompletedConsumer : IConsumer<DepositCompleted>
    {
        private readonly ILogger<DepositCompletedConsumer> _logger;
        private readonly DepositService _depositService;
        private readonly HotWalletService _hotWalletService;
        private readonly DepositWalletService _depositWalletService;

        public DepositCompletedConsumer(ILogger<DepositCompletedConsumer> logger,
            DepositService depositService,
            HotWalletService hotWalletService,
            DepositWalletService depositWalletService)
        {
            _logger = logger;
            _depositService = depositService;
            _hotWalletService = hotWalletService;
            _depositWalletService = depositWalletService;
        }

        public async Task Consume(ConsumeContext<DepositCompleted> context)
        {
            var @event = context.Message;

            _logger.LogInformation("Deposit processing has been started {@event}", @event);

            if (@event == null)
            {
                _logger.LogInformation("Deposit completed (empty message body)");

                return;
            }

            var depositWallet = await _depositWalletService.GetByIdAsync(@event.BlockchainId, @event.NetworkId, @event.WalletId);

            if (depositWallet != null)
            {
                var deposit = MapFromMessage(@event);
                await _depositService.AddAsync(deposit);

                _logger.LogInformation("Deposit completed {@event}", @event);
                
                return;
            }

            var hotWallet = await _hotWalletService.GetByIdAsync(@event.BlockchainId, @event.NetworkId, @event.WalletId);

            //More likely this is transfer from deposit to hotwallet
            //Consolidation happens here
            if (hotWallet != null)
            {
                var deposit = MapFromMessage(@event);
                await _depositService.AddAsync(deposit);

                _logger.LogInformation("Deposit completed {@event}", @event);
                return;
            }

            _logger.LogInformation("Deposit completed (unknown wallet)");
        }

        private Deposit MapFromMessage(DepositCompleted @event)
        {
            return new Deposit()
            {
                Sources = @event.Sources?.Select(x => new Domain.Deposits.DepositSource()
                {
                    Address = x.Address,
                    Amount = x.Amount
                }).ToArray(),
                WalletId = @event.WalletId,
                BlockchainId = @event.BlockchainId,
                NetworkId = @event.NetworkId,
                Id = @event.Id,
                TransactionHash = @event.TransactionHash,
                AssetId = @event.AssetId
            };
        }
    }
}
