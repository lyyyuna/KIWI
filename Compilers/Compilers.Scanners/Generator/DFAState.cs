using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Compilers.Scanners.Generator
{
    public class DFAState
    {
        private List<DFAEdge> m_OutEdges;
        private SortedSet<int> m_nfastateSet;

        public int Index
        {
            get;
            internal set;
        }

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

        public ReadOnlyCollection<DFAEdge> OutEdges
        {
            get
            {
                return m_OutEdges.AsReadOnly();
            }
        }

        internal void AddEdge(DFAEdge edge)
        {
            m_OutEdges.Add(edge);

        }
    }
}
