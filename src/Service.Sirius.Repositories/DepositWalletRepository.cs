using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Sirius.Repositories.DbContexts;
using Service.Sirius.Repositories.Entities;
using Sirius.Domain.DepositWallets;
using Sirius.Domain.Repositories;

namespace Service.Sirius.Repositories
{
    public class DepositWalletRepository : IDepositWalletRepository
    {
        private readonly DbContextOptionsBuilder<SiriusContext> _dbContextOptionsBuilder;

        public DepositWalletRepository(DbContextOptionsBuilder<SiriusContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        public async Task AddOrUpdateAsync(DepositWallet depositWallet)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);

            var group = await context.WalletGroups.FindAsync(depositWallet.BlockchainId, depositWallet.NetworkId,
                depositWallet.GroupName);

            if (group == null)
            {
                group = new WalletGroupEntity()
                {
                    BlockchainId = depositWallet.BlockchainId,
                    NetworkId = depositWallet.NetworkId,
                    GroupName = depositWallet.GroupName,
                };

                context.WalletGroups.Add(group);
            }

            var existing = await context.DepositWallets
                .FindAsync(depositWallet.BlockchainId, depositWallet.NetworkId, depositWallet.Id);

            if (existing != null)
                return;

            var entity = MapToEntity(depositWallet);
            context.DepositWallets.Add(entity);
            group.DepositWallets.Add(entity);

            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<DepositWallet>> GetManyAsync(string blockchainId, string networkId, int startAfter, int limit)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);

            var many = context
                .DepositWallets
                .Include(x => x.WalletGroup)
                .Where(x => x.BlockchainId == blockchainId &&
                            x.NetworkId == networkId)
                .Skip(startAfter)
                .Take(limit);

            await many.LoadAsync();

            return many.AsEnumerable()
                .Select(MapToDomain)
                .ToArray();
        }

        public async Task<DepositWallet> GetByIdAsync(string blockchainId, string networkId, string walletId)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);

            var existing = await context
                .DepositWallets
                .Include(x => x.WalletGroup)
                .FirstOrDefaultAsync(x => x.BlockchainId == blockchainId &&
                                          x.NetworkId == networkId &&
                                          x.Id == walletId);

            return MapToDomain(existing);
        }

        private static DepositWalletEntity MapToEntity(DepositWallet depositWallet)
        {
            if (depositWallet == null)
                return null;

            return new DepositWalletEntity()
            {
                PublicKey = depositWallet.PublicKey,
                Id = depositWallet.Id,
                BlockchainId = depositWallet.BlockchainId,
                NetworkId = depositWallet.NetworkId,
                OriginalWalletAddress = depositWallet.Address,
                WalletAddress = depositWallet.Address.ToLowerInvariant()
            };
        }

        private static DepositWallet MapToDomain(DepositWalletEntity depositWalletEntity)
        {
            if (depositWalletEntity == null)
                return null;

            return new DepositWallet()
            {
                PublicKey = depositWalletEntity.PublicKey,
                Id = depositWalletEntity.Id,
                BlockchainId = depositWalletEntity.BlockchainId,
                NetworkId = depositWalletEntity.NetworkId,
                GroupName = depositWalletEntity.GroupName,
                Address = depositWalletEntity.OriginalWalletAddress,
            };
        }
    }
}
