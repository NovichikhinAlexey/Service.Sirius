using System.Collections.Generic;
using System.Numerics;

namespace Sirius.Domain.Blockchains
{
    public sealed class DepositTagsCapabilities
    {
        public bool Number { get; set; }
        public BigInteger MinNumber { get; set; }
        public BigInteger MaxNumber { get; set; }
        public bool Text { get; set; }
        public int MaxTextLength { get; set; }
        public IReadOnlyDictionary<string, string> NumberTagNames { get; set; }
        public IReadOnlyDictionary<string, string> TextTagNames { get; set; }
    }
}
