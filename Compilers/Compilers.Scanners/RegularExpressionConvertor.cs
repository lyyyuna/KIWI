﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public abstract class RegularExpressionConvertor<T>
    {
        protected RegularExpressionConvertor()
        { }

        public virtual T Convert(RegularExpression expression)
        {
            if (expression == null)
            {
                return default(T);
            }

            switch (expression.ExpressionType)
            {
                case  RegularExpressionType.Empty:
                    return ConvertEmpty(expression as EmptyExpression);
                case RegularExpressionType.Symbol:
                    return ConvertSymbol(expression as SymbolExpression);
                case RegularExpressionType.Alternation:
                    return ConvertAlternation(expression as AlternationExpression);
                case RegularExpressionType.Concatenation:
                    return ConvertConcatenation(expression as ConcatenationExpression);
                case RegularExpressionType.KleenStar:
                    return ConvertKleeneStar(expression as KleeneStarExpression);
                case RegularExpressionType.AlternationCharSet:
                    return ConvertAlternationCharSet(expression as AlternationCharSetExpression);
                case RegularExpressionType.StringLiteral:
                    return ConvertStringLiteral(expression as StringLiteralExpression);
                default:
                    throw new ArgumentException("The expression type is not recognized", "expression");
            }
            
        }


        public abstract T ConvertAlternation(AlternationExpression exp);
        public abstract T ConvertSymbol(SymbolExpression exp);
        public abstract T ConvertEmpty(EmptyExpression exp);
        public abstract T ConvertConcatenation(ConcatenationExpression exp);
        public abstract T ConvertAlternationCharSet(AlternationCharSetExpression exp);
        public abstract T ConvertStringLiteral(StringLiteralExpression exp);
        public abstract T ConvertKleeneStar(KleeneStarExpression exp);
    }
}
