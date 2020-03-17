using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Sirius.Repositories.Entities
{
    [Table(name: Tables.HotWalletsTableName)]
    public class HotWalletEntity
    {
        [Key, Column(Order = 0)]
        public string BlockchainId { get; set; }

        [Key, Column(Order = 1)]
        public string NetworkId { get; set; }

        [Key, Column(Order = 2)]
        public string Id { get; set; }

        public string WalletAddress { get; set; }

        public string OriginalWalletAddress { get; set; }

        public string PublicKey { get; set; }

        //[ForeignKey(Tables.WalletGroupsTableName)]
        public string GroupName { get; set; }
        public WalletGroupEntity WalletGroup { get; set; }
    }
}
