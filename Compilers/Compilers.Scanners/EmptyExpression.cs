using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class EmptyExpression: RegularExpression
    {
        // singleton
        private EmptyExpression()
            : base(RegularExpressionType.Empty)
        { }

        public static readonly EmptyExpression Instance = new EmptyExpression();
    }
}
