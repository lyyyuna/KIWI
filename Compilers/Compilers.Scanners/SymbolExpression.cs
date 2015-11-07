﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class SymbolExpression: RegularExpression
    {
        public new char Symbol { get; private set; }


        public SymbolExpression(char c): base(RegularExpressionType.Symbol)
        {
            // TODO: Complete member initialization
            this.Symbol = c;
        }
    }
}
