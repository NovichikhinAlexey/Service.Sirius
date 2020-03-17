using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.BlockchainWalletApi.Client.Http;
using Sirius.Domain.Repositories;

namespace Sirius.Domain.Withdrawals
{
    public class WithdrawalService
    {
        private readonly IBlockchainWalletClient _blockchainWalletClient;
        private readonly IWithdrawalRepository _withdrawalRepository;

        public WithdrawalService(
            IBlockchainWalletClient blockchainWalletClient,
            IWithdrawalRepository withdrawalRepository)
        {
            _blockchainWalletClient = blockchainWalletClient;
            _withdrawalRepository = withdrawalRepository;
        }

        public async Task Execute(
            Guid requestId,
            string blockchainId,
            string networkId,
            string fromAddress,
            string toAddress,
            string assetId,
            decimal amount)
        {
            var result = await _blockchainWalletClient.ExecuteTransferAsync(
                requestId, 
                blockchainId, 
                networkId, 
                new ExecuteTransferRequest
                {
                    Destinations = new List<TransferDestination>
                    {
                        new TransferDestination
                        {
                            Address = toAddress,
                            Units = new List<TransferUnit>
                            {
                                new TransferUnit
                                {
                                    Amount = amount,
                                    AssetId = assetId
                                }
                            }
                        }
                    },
                    Sources = new List<TransferSource>
                    {
                        new TransferSource
                        {
                            Address = fromAddress,
                            Nonce = 0,
                        }
                    },
                });
        }

        public async Task SaveWithdrawalAsync(Withdrawal withdrawal)
        {
            await _withdrawalRepository.SaveAsync(withdrawal);
        }

        public Task<IReadOnlyCollection<Withdrawal>> GetManyAsync(string blockchainId, string networkId, int startingAfter, int limit)
        {
            return _withdrawalRepository.GetManyAsync(blockchainId, networkId, startingAfter, limit);
        }
    }

    public class Withdrawal
    {
    }
}
