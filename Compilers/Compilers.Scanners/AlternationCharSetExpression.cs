using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class AlternationCharSetExpression: RegularExpression
    {
        private IEnumerable<char> charSet;

        public AlternationCharSetExpression(IEnumerable<char> charSet): base(RegularExpressionType.AlternationCharSet)
        {
            // TODO: Complete member initialization
            this.charSet = charSet;
        }
    }
}
