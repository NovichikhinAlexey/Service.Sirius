using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sirius.Domain.Repositories;

namespace Sirius.Domain.Deposits
{
    public class Deposit
    {
        public string BlockchainId { get; set; }

        public string NetworkId { get; set; }
        public string Id { get; set; }
        public IReadOnlyCollection<DepositSource> Sources { get; set; }
        public string WalletId { get; set; }
        public string TransactionHash { get; set; }
        public string AssetId { get; set; }
    }

    public class DepositSource
    {
        public string Address { get; set; }

        public decimal Amount { get; set; }
    }

    public class DepositService
    {
        private readonly IDepositsRepository _depositsRepository;

        public DepositService(IDepositsRepository depositsRepository)
        {
            _depositsRepository = depositsRepository;
        }

        public async Task<Deposit> GetByIdAsync(string blockchainId, string networkId, string id)
        {
            return await _depositsRepository.GetAsync(blockchainId, networkId, id);
        }

        public async Task<IReadOnlyCollection<Deposit>> GetManyAsync(string blockchainId, string networkId, int startFrom, int limit)
        {
            return await _depositsRepository.GetManyAsync(blockchainId, networkId, startFrom, limit);
        }

        public async Task AddAsync(Deposit deposit)
        {
            await _depositsRepository.AddAsync(deposit);
        }
    }
}
