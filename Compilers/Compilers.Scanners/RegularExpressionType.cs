using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public enum RegularExpressionType
    {
        Empty,
        Symbol,
        Alternation,
        Concatenation,
        KleenStar,
        AlternationCharSet,
        StringLiteral
    }
}
