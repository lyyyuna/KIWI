using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class StringLiteralExpression: RegularExpression
    {
        public new string Literal { get; private set; }

        public StringLiteralExpression(string literal): base(RegularExpressionType.StringLiteral)
        {
            // TODO: Complete member initialization
            this.Literal = literal;
        }
    }
}
