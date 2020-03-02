using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.Transactions
{
    public sealed class ProceedUnsignedTransactionRequest
    {
        [FromHeader(Name = "X-Request-ID"), Required]
        public Guid RequestId { get; set; }

        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }

        [FromRoute(Name = "id")]
        public string Id { get; set; }

        [FromBody]
        public string SignedTransaction { get; set; }
    }
}
