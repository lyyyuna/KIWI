using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class StringLiteralExpression: RegularExpression
    {
        private string literal;

        public StringLiteralExpression(string literal): base(RegularExpressionType.StringLiteral)
        {
            // TODO: Complete member initialization
            this.literal = literal;
        }
    }
}
