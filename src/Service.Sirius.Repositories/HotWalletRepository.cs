using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Sirius.Repositories.DbContexts;
using Service.Sirius.Repositories.Entities;
using Sirius.Domain.HotWallets;
using Sirius.Domain.Repositories;

namespace Service.Sirius.Repositories
{
    public class HotWalletRepository : IHotWalletRepository
    {
        private readonly DbContextOptionsBuilder<SiriusContext> _dbContextOptionsBuilder;

        public HotWalletRepository(DbContextOptionsBuilder<SiriusContext> dbContextOptionsBuilder)
        {
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
        }

        public async Task DesignateAsync(string blockchainId, string networkId, string groupName, string id)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);
            var existingGroup = await context.WalletGroups.FindAsync(blockchainId, networkId, groupName);

            if (existingGroup == null)
            {
                existingGroup = new WalletGroupEntity()
                {
                    GroupName = groupName
                };

                context.WalletGroups.Add(existingGroup);
            }

            var existingHotwallet = await context.HotWallets.FindAsync(
                blockchainId,
                networkId,
                id);

            if (existingHotwallet == null)
                return;

            existingHotwallet.WalletGroup = existingGroup;
            existingGroup.HotWallet = existingHotwallet;

            context.Update(existingHotwallet);

            await context.SaveChangesAsync();
        }

        public async Task<HotWallet> GetDesignatedAsync(string blockchainId, string networkId, string groupName)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);
            var existingGroup = await context
                .WalletGroups
                .Include(x => x.HotWallet)
                .FirstOrDefaultAsync(x =>
                    x.BlockchainId == blockchainId &&
                    x.NetworkId == networkId &&
                    x.GroupName == groupName);

            if (existingGroup == null)
                return null;

            return MapToDomain(existingGroup.HotWallet);
        }

        public async Task AddOrUpdateAsync(HotWallet hotWallet)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);
            var existingGroup = await context
                .WalletGroups
                .FindAsync(hotWallet.BlockchainId, hotWallet.NetworkId, hotWallet.GroupName);

            if (existingGroup == null)
            {
                existingGroup = new WalletGroupEntity()
                {
                    BlockchainId = hotWallet.BlockchainId,
                    NetworkId = hotWallet.NetworkId,
                    GroupName = hotWallet.GroupName
                };

                context.WalletGroups.Add(existingGroup);
            }

            var existing = await context.HotWallets.FindAsync(
                hotWallet.BlockchainId,
                hotWallet.NetworkId,
                hotWallet.Id);

            if (existing == null)
            {
                var newEntity = MapToEntity(hotWallet);

                newEntity.WalletGroup = existingGroup;
                existingGroup.HotWallet = newEntity;
                existingGroup.WalletId = newEntity.Id;

                context.Add(newEntity);
            }

            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<HotWallet>> GetAllAsync(string blockchainId, string networkId, int startAfter, int limit)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);
            var many =
                context.HotWallets
                    .Include(x => x.WalletGroup)
                    .Where(x => x.BlockchainId == blockchainId &&
                                x.NetworkId == networkId)
                    .OrderBy(x => x.Id)
                    .Skip(startAfter)
                    .Take(limit);

            await many.LoadAsync();

            return many.AsEnumerable().Select(MapToDomain).ToArray();
        }

        public async Task<HotWallet> GetAsync(string blockchainId, string networkId, string walletId)
        {
            await using var context = new SiriusContext(_dbContextOptionsBuilder.Options);
            var existing =
                await context.HotWallets
                    .Include(x => x.WalletGroup)
                    .FirstOrDefaultAsync(x => x.BlockchainId == blockchainId &&
                                              x.NetworkId == networkId &&
                                              x.Id == walletId);

            return MapToDomain(existing);
        }

        private static HotWalletEntity MapToEntity(HotWallet hotWallet)
        {
            if (hotWallet == null)
                return null;

            return new HotWalletEntity()
            {
                PublicKey = hotWallet.PublicKey,
                Id = hotWallet.Id,
                BlockchainId = hotWallet.BlockchainId,
                NetworkId = hotWallet.NetworkId,
                OriginalWalletAddress = hotWallet.Address,
                WalletAddress = hotWallet.Address.ToLowerInvariant()
            };
        }

        private static HotWallet MapToDomain(HotWalletEntity hotWalletEntity)
        {
            if (hotWalletEntity == null)
                return null;

            return new HotWallet()
            {
                PublicKey = hotWalletEntity.PublicKey,
                Id = hotWalletEntity.Id,
                BlockchainId = hotWalletEntity.BlockchainId,
                NetworkId = hotWalletEntity.NetworkId,
                GroupName = hotWalletEntity.WalletGroup.GroupName,
                Address = hotWalletEntity.OriginalWalletAddress
            };
        }
    }
}
