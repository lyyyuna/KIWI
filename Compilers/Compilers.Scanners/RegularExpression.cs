using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Compilers.Scanners
{
    public abstract class RegularExpression
    {
        public RegularExpressionType ExpressionType { get; private set; }

        protected RegularExpression(RegularExpressionType expressionType)
        {
            ExpressionType = expressionType;
        }

        
        // basic regex operation

        // a
        public static RegularExpression Symbol(char c)
        {
            return new SymbolExpression(c);
        }

        // a*
        public RegularExpression Many()
        {
            if (ExpressionType == RegularExpressionType.KleenStar)
            {
                return this;
            }

            return new KleeneStarExpression(this);
        }

        // ab
        public RegularExpression Concat(RegularExpression next)
        {
            return new ConcatenationExpression(this, next);
        }


        // a|b
        public RegularExpression Union(RegularExpression other)
        {
            if (this.Equals(other))
            {
                return this;
            }

            return new AlternationExpression(this, other);
        }

        // abc
        public static RegularExpression Literal(string literal)
        {
            return new StringLiteralExpression(literal);
        }

        // [abc]
        public static RegularExpression CharSet(IEnumerable<char> charSet)
        {
            return new AlternationCharSetExpression(charSet);
        }

        // epsilon
        // e
        public static RegularExpression Empty()
        {
            return EmptyExpression.Instance;
        }

        // overload some operators to provide regex-like operation

        // a|b
        public static RegularExpression operator|(RegularExpression left, RegularExpression right)
        {
            return new AlternationExpression(left, right);
        }

        // xy
        // x>>y
        [SpecialName]
        public static RegularExpression op_RightShift(RegularExpression left, RegularExpression right)
        {
            return new ConcatenationExpression(left, right);
        }
    }
}
