using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class SymbolExpression: RegularExpression
    {
        public char Symbol { get; private set; }


        public SymbolExpression(char c): base(RegularExpressionType.Symbol)
        {
            // TODO: Complete member initialization
            this.Symbol = c;
        }
    }
}
