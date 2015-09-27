using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    class KleeneStarExpression: RegularExpression
    {
        private RegularExpression regularExpression;

        public KleeneStarExpression(RegularExpression regularExpression): base(RegularExpressionType.KleenStar)
        {
            // TODO: Complete member initialization
            this.regularExpression = regularExpression;
        }
    }
}
