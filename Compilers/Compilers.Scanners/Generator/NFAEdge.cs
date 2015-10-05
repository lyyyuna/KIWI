using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public struct NFAEdge
    {
        public char? Symbol { get; private set; }
        public NFAState Targetstate { get; private set; }

        public NFAEdge(char symbol, NFAState targetState):this()
        {
            this.Symbol = symbol;
            this.Targetstate = targetState;
        }

        public NFAEdge(NFAState targetState) : this()
        {
            this.Targetstate = targetState;
        }

        public bool IsEmpty
        { get { return !this.Symbol.HasValue; } }
    }
}
