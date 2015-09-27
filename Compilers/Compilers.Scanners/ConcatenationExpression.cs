using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class ConcatenationExpression: RegularExpression
    {
        private RegularExpression regularExpression;
        private RegularExpression next;

        public ConcatenationExpression(RegularExpression regularExpression, RegularExpression next): base(RegularExpressionType.Concatenation)
        {
            // TODO: Complete member initialization
            this.regularExpression = regularExpression;
            this.next = next;
        }
    }
}
