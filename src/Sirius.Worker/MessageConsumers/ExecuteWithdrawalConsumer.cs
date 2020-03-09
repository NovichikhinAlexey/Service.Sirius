using System.Threading.Tasks;
using MassTransit;
using Sirius.Domain.Withdrawals;

namespace Sirius.Worker.MessageConsumers
{
    public class ExecuteWithdrawalConsumer : IConsumer<ExecuteWithdrawal>
    {
        //private readonly WithdrawalService _withdrawalService;

        //public ExecuteWithdrawalConsumer(WithdrawalService withdrawalService)
        //{
        //    _withdrawalService = withdrawalService;
        //}

        public async Task Consume(ConsumeContext<ExecuteWithdrawal> context)
        {
            var command = context.Message;

            //await _withdrawalService.Execute(
            //    command.RequestId,
            //    command.BlockchainId,
            //    command.NetworkId,
            //    command.HotWalletAddress,
            //    command.DestinationAddress,
            //    command.AssetId,
            //    command.Amount);
        }
    }
}
