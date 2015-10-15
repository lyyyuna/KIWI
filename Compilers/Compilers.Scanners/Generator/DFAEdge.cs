using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public struct DFAEdge
    {
        public char Symbol { get; private set; }
        public DFAState TargetState { get; private set; }
    }
}
