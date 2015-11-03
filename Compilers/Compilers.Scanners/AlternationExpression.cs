using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class AlternationExpression: RegularExpression
    {
        public RegularExpression Expression1 {get; private set;}
        public RegularExpression Expression2 {get; private set;}

        public AlternationExpression(RegularExpression regularExpression, RegularExpression other): base(RegularExpressionType.Alternation)
        {
            CodeContract.RequireArgumentNotNull(regularExpression, "expression1");
            CodeContract.RequireArgumentNotNull(other, "expression2");


            this.Expression1 = regularExpression;
            this.Expression2 = other;
        }
    }
}
