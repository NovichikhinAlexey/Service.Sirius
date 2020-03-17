using System.Collections.Generic;
using System.Threading.Tasks;
using Sirius.Domain.Repositories;
using Sirius.Domain.Withdrawals;

namespace Service.Sirius.Repositories
{
    public class WithdrawalRepository : IWithdrawalRepository
    {
        public WithdrawalRepository()
        {
            
        }
        public Task<IReadOnlyCollection<Withdrawal>> GetManyAsync(string blockchainId, string networkId, in int startingAfter, in int limit)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync(Withdrawal withdrawal)
        {
            throw new System.NotImplementedException();
        }
    }
}
