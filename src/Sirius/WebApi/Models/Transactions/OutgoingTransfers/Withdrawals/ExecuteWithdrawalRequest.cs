using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Sirius.Domain;

namespace Sirius.WebApi.Models.Transactions.OutgoingTransfers.Withdrawals
{
    public sealed class ExecuteWithdrawalRequest
    {
        [FromHeader(Name = "X-Request-ID"), Required]
        public Guid RequestId { get; set; }

        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }

        [FromBody]
        public string HotWalletId { get; set; }

        [FromBody]
        public string AssetId { get; set; }

        [FromBody]
        public decimal Amount { get; set; }

        [FromBody]
        public string DestinationAddress { get; set; }

        [FromBody]
        public string DestinationTag { get; set; }

        [FromBody]
        public DestinationTagType? DestinationTagType { get; set; }

        [FromBody]
        public FeePayer FeePayer { get; set; }

        [FromBody]
        public FeeStrategy? FeeStrategy { get; set; }

        [FromBody]
        public bool SkipBatching { get; set; }
    }
}
