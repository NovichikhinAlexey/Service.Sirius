using System.Collections.Generic;
using System.Threading.Tasks;
using Sirius.Domain.Deposits;

namespace Sirius.Domain.Repositories
{
    public interface IDepositsRepository
    {
        Task<Deposit> GetAsync(string blockchainId, string networkId, string id);
        Task AddAsync(Deposit deposit);
        Task<IReadOnlyCollection<Deposit>> GetManyAsync(string blockchainId, string networkId, int startFrom, int limit);
    }
}
