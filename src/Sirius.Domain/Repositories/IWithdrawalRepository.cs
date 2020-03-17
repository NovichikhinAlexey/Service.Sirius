using System.Collections.Generic;
using System.Threading.Tasks;
using Sirius.Domain.Withdrawals;

namespace Sirius.Domain.Repositories
{
    public interface IWithdrawalRepository
    {
        Task<IReadOnlyCollection<Withdrawal>> GetManyAsync(string blockchainId, string networkId, in int startingAfter, in int limit);
        Task SaveAsync(Withdrawal withdrawal);
    }
}
