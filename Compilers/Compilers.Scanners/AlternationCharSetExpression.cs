using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Compilers.Scanners
{
    public sealed class AlternationCharSetExpression: RegularExpression
    {
        private List<char> m_charSet;

        public AlternationCharSetExpression(IEnumerable<char> charSet): base(RegularExpressionType.AlternationCharSet)
        {
            // TODO: Complete member initialization
            this.m_charSet = new List<char>(charSet);
        }

        public new ReadOnlyCollection<char> CharSet
        {
            get
            {
                return m_charSet.AsReadOnly();
            }
        }
    }
}
