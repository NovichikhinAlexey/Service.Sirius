using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Sirius.Repositories.DbContexts;
using Service.Sirius.Repositories.Entities;
using Sirius.Domain.Deposits;
using Sirius.Domain.Repositories;

namespace Service.Sirius.Repositories
{
    public class DepositRepository : IDepositsRepository
    {
        private readonly DbContextOptionsBuilder<SiriusContext> _dbContextOptionsBuilder;

        public DepositRepository(DbContextOptionsBuilder<SiriusContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }


        public async Task<Deposit> GetAsync(string blockchainId, string networkId, string id)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);

            var entity = await context
                .Deposits
                .Include(x => x.DepositSources)
                .FirstOrDefaultAsync(x => x.BlockchainId == blockchainId &&
                                          x.NetworkId == networkId &&
                                          x.Id == id);

            return MapToDomain(entity);
        }

        public async Task<IReadOnlyCollection<Deposit>> GetManyAsync(string blockchainId, string networkId, int startFrom, int limit)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);
            var many = context
                .Deposits
                .Include(x => x.DepositSources)
                .Where(x => x.BlockchainId == blockchainId &&
                            x.NetworkId == networkId)
                .Skip(startFrom)
                .Take(limit);

            await many.LoadAsync();

            return many
                .AsEnumerable()
                .Select(MapToDomain)
                .ToArray();

        }

        public async Task AddAsync(Deposit deposit)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);
            var existing = await context.Deposits.FindAsync(deposit.BlockchainId, deposit.NetworkId, deposit.Id);

            if (existing != null)
                return;

            var newEntity = MapToEntity(deposit);

            context.Deposits.Add(newEntity);
            context.DepositSources.AddRange(newEntity.DepositSources);

            await context.SaveChangesAsync();
        }

        private DepositEntity MapToEntity(Deposit deposit)
        {
            if (deposit == null)
                return null;

            var depositEntity = new DepositEntity()
            {
                BlockchainId = deposit.BlockchainId,
                Id = deposit.Id,
                AssetId = deposit.AssetId,
                TransactionHash = deposit.TransactionHash,
                NetworkId = deposit.NetworkId,
                WalletId = deposit.WalletId
            };

            depositEntity.DepositSources = (ICollection<DepositSourceEntity>) 
                deposit.Sources?.Select(x => MapToEntity(depositEntity, x)) ?? new List<DepositSourceEntity>();

            return depositEntity;
        }

        private DepositSourceEntity MapToEntity(DepositEntity depositEntity, DepositSource depositSource)
        {
            if (depositSource == null)
                return null;

            return new DepositSourceEntity()
            {
                Amount = depositSource.Amount,
                Deposit = depositEntity,
                SourceAddress = depositSource.Address
            };
        }

        private Deposit MapToDomain(DepositEntity depositEntity)
        {
            if (depositEntity == null)
                return null;

            var deposit = new Deposit()
            {
                BlockchainId = depositEntity.BlockchainId,
                Id = depositEntity.Id,
                AssetId = depositEntity.AssetId,
                TransactionHash = depositEntity.TransactionHash,
                NetworkId = depositEntity.NetworkId,
                WalletId = depositEntity.WalletId,
                Sources = depositEntity.DepositSources.Select(x => new DepositSource()
                {
                    Address = x.SourceAddress,
                    Amount = x.Amount
                }).ToArray()
            };

            return deposit;
        }
    }
}
