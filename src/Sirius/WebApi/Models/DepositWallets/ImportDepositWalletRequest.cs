using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.DepositWallets
{
    public sealed class ImportDepositWalletRequest
    {
        [FromBody]
        public string GroupName { get; set; }

        [FromBody]
        public string Address { get; set; }

        [FromBody]
        public string PublicKey { get; set; }
    }
}
