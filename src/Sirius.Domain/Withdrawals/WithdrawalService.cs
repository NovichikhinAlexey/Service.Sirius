using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.BlockchainWalletApi.Client.Http;

namespace Sirius.Domain.Withdrawals
{
    public class WithdrawalService
    {
        private readonly IBlockchainWalletClient _blockchainWalletClient;

        public WithdrawalService(IBlockchainWalletClient blockchainWalletClient)
        {
            _blockchainWalletClient = blockchainWalletClient;
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
    }
}
