using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class AlternationExpression: RegularExpression
    {
        private RegularExpression regularExpression;
        private RegularExpression other;

        public AlternationExpression(RegularExpression regularExpression, RegularExpression other): base(RegularExpressionType.Alternation)
        {
            // TODO: Complete member initialization
            this.regularExpression = regularExpression;
            this.other = other;
        }
    }
}
