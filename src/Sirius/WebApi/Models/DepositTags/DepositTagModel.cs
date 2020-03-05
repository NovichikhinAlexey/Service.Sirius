using Sirius.WebApi.Models.Transactions;

namespace Sirius.WebApi.Models.DepositTags
{
    public sealed class DepositTagModel
    {
        public string Tag { get; set; }
        public DestinationTagType TagType { get; set; } 
        public string GroupName { get; set; }
        public string UserContext { get; set; }
    }
}
