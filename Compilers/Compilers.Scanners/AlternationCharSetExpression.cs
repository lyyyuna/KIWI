using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class AlternationCharSetExpression: RegularExpression
    {
        private List<char> m_charSet;

        public AlternationCharSetExpression(IEnumerable<char> charSet): base(RegularExpressionType.AlternationCharSet)
        {
            // TODO: Complete member initialization
            this.m_charSet = new List<char>(charSet);
        }
    }
}
