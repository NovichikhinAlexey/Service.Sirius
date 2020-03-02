﻿using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.HotWallets
{
    public sealed class ImportHotWalletRequest
    {
        [FromRoute(Name = "blockchainId")]
        public string BlockchainId { get; set; }

        [FromRoute(Name = "networkId")]
        public string NetworkId { get; set; }

        [FromBody]
        public string GroupName { get; set; }

        [FromBody]
        public string Address { get; set; }

        [FromBody]
        public string PublicKey { get; set; }
    }
}
