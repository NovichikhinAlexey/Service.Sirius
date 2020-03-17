using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Sirius.Repositories.Entities
{
    [Table(name: Tables.DepositWalletsTableName)]
    public class DepositWalletEntity
    {
        [Key, ForeignKey(Tables.WalletGroupsTableName), Column(Order = 0)]
        public string BlockchainId { get; set; }

        [Key, ForeignKey(Tables.WalletGroupsTableName), Column(Order = 1)]
        public string NetworkId { get; set; }

        [Key, Column(Order = 2)]
        public string Id { get; set; }

        //Index
        public string WalletAddress { get; set; }

        public string OriginalWalletAddress { get; set; }

        public string PublicKey { get; set; }

        [ForeignKey(Tables.WalletGroupsTableName)]
        public string GroupName { get; set; }
        public WalletGroupEntity WalletGroup { get; set; }
    }
}
