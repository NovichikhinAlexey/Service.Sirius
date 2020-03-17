using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Service.Sirius.Repositories.Entities;
using Sirius.Domain.HotWallets;

namespace Service.Sirius.Repositories.DbContexts
{
    public class SiriusContext : DbContext
    {
        public SiriusContext(DbContextOptions<SiriusContext> options) :
            base(options)
        {
        }

        public DbSet<HotWalletEntity> HotWallets { get; set; }

        public DbSet<DepositWalletEntity> DepositWallets { get; set; }

        public DbSet<DepositEntity> Deposits { get; set; }
        
        public DbSet<DepositSourceEntity> DepositSources { get; set; }

        public DbSet<WalletGroupEntity> WalletGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(PostgresRepositoryConfiguration.SchemaName);


            modelBuilder.Entity<DepositWalletEntity>()
                .HasKey(c => new { c.BlockchainId, c.NetworkId, c.Id });

            modelBuilder
                .Entity<DepositWalletEntity>()
                .HasIndex(c => new { c.BlockchainId, c.NetworkId, c.WalletAddress })
                .IsUnique(true)
                .HasName("IX_DW_BlockchainId_NetworkId_WalletAddress");

            modelBuilder
                .Entity<DepositWalletEntity>()
                .HasOne(x => x.WalletGroup)
                .WithMany(x => x.DepositWallets)
                .HasForeignKey(x => new { x.BlockchainId, x.NetworkId, x.GroupName });


            modelBuilder.Entity<HotWalletEntity>()
                .HasKey(c => new { c.BlockchainId, c.NetworkId, c.Id});


            modelBuilder
                .Entity<HotWalletEntity>()
                .HasIndex(c => new { c.BlockchainId, c.NetworkId, c.WalletAddress})
                .IsUnique(true)
                .HasName("IX_HW_BlockchainId_NetworkId_WalletAddress");

            modelBuilder
                .Entity<HotWalletEntity>()
                .HasOne(x => x.WalletGroup)
                .WithOne(x => x.HotWallet)
                .HasForeignKey<WalletGroupEntity>(x => new {x.BlockchainId, x.NetworkId, x.WalletId});

            modelBuilder.Entity<WalletGroupEntity>()
                .HasKey(c => new { c.BlockchainId, c.NetworkId, c.GroupName });

            modelBuilder.Entity<WalletGroupEntity>()
                .HasOne(x => x.HotWallet)
                .WithOne(x => x.WalletGroup)
                .HasForeignKey<HotWalletEntity>(x => new { x.BlockchainId, x.NetworkId, x.GroupName});


            modelBuilder.Entity<DepositEntity>()
                .HasKey(c => new { c.BlockchainId, c.NetworkId, c.Id });

            modelBuilder
                .Entity<DepositEntity>()
                .HasIndex(c => new { c.BlockchainId, c.NetworkId, c.TransactionHash })
                .IsUnique(true)
                .HasName("IX_BlockchainId_NetworkId_TransactionHash");

            base.OnModelCreating(modelBuilder);
        }
    }
}
