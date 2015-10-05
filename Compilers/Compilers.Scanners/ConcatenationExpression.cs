using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class ConcatenationExpression: RegularExpression
    {
        public RegularExpression Left {get; private set;}
        public RegularExpression Right { get; private set; }

        public ConcatenationExpression(RegularExpression regularExpression, RegularExpression next): base(RegularExpressionType.Concatenation)
        {
            // TODO: Complete member initialization
            this.Left = regularExpression;
            this.Right = next;
        }
    }
}
