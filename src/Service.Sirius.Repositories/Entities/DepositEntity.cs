using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Sirius.Repositories.Entities
{
    [Table(name: Tables.DepositsTable)]
    public class DepositEntity
    {
        public DepositEntity()
        {
            DepositSources = new HashSet<DepositSourceEntity>();
        }

        [Key, Column(Order = 0)]
        public string BlockchainId { get; set; }

        [Key, Column(Order = 1)]
        public string NetworkId { get; set; }

        [Key, Column(Order = 2)]
        public string Id { get; set; }

        public string TransactionHash { get; set; }

        public string AssetId { get; set; }

        public ICollection<DepositSourceEntity> DepositSources { get; set; }
        public string WalletId { get; set; }
    }

    [Table(name: Tables.DepositSourcesTable)]
    public class DepositSourceEntity
    {
        public DepositEntity Deposit { get; set; }

        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string SourceAddress { get; set; }

        public decimal Amount { get; set; }
    }
}
