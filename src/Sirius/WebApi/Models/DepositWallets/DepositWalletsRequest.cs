using Microsoft.AspNetCore.Mvc;

namespace Sirius.WebApi.Models.DepositWallets
{
    public sealed class DepositWalletsRequest : PaginationRequest<string>
    {
        [FromQuery]
        public string GroupName { get; set; }
    }
}
