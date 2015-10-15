using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public class DFAState
    {
        private List<DFAEdge> m_OutEdges;
        private SortedSet<int> m_nfastateSet;

        internal DFAState()
        {
            m_OutEdges = new List<DFAEdge>();
            m_nfastateSet = new SortedSet<int>();
        }

        public ISet<int> NFAStateSet
        {
            get
            {
                return m_nfastateSet;
            }
        }

        internal void AddEdge(DFAEdge edge)
        {
            m_OutEdges.Add(edge);
        }
    }
}
