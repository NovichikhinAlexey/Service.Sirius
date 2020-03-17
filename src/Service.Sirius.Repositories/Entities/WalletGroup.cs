using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Sirius.Repositories.Entities
{
    [Table(Tables.WalletGroupsTableName)]
    public class WalletGroupEntity
    {
        public WalletGroupEntity()
        {
            DepositWallets = new HashSet<DepositWalletEntity>();
        }

        [Key, Column(Order = 0)]
        public string BlockchainId { get; set; }

        [Key, Column(Order = 1)]
        public string NetworkId { get; set; }

        [Key, Column(Order = 2)]
        public string GroupName { get; set; }
        
        //[ForeignKey(Tables.HotWalletsTableName)]
        public string WalletId { get; set; }

        public HotWalletEntity HotWallet { get; set; }

        public ICollection<DepositWalletEntity> DepositWallets { get; set; }
    }
}
