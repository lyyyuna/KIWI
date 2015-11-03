using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class ConcatenationExpression: RegularExpression
    {
        public RegularExpression Left {get; private set;}
        public RegularExpression Right { get; private set; }

        public ConcatenationExpression(RegularExpression regularExpression, RegularExpression next): base(RegularExpressionType.Concatenation)
        {
            CodeContract.RequireArgumentNotNull(regularExpression, "left");
            CodeContract.RequireArgumentNotNull(next, "right");


            this.Left = regularExpression;
            this.Right = next;
        }
    }
}
