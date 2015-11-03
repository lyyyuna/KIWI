using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class KleeneStarExpression: RegularExpression
    {
        public RegularExpression InnerExpression { get; private set; }

        public KleeneStarExpression(RegularExpression regularExpression): base(RegularExpressionType.KleenStar)
        {
            CodeContract.RequireArgumentNotNull(regularExpression, "innerExp");


            this.InnerExpression = regularExpression;
        }
    }
}
