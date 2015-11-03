using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Compilers.Scanners.Generator
{
    [DebuggerDisplay("State #{Index}")]
    public class NFAState
    {
        private List<NFAEdge> m_OutEdges;
        public int Index { get; internal set; }

        internal NFAState()
        {
            this.m_OutEdges = new List<NFAEdge>();
        }

        public ReadOnlyCollection<NFAEdge> OutEdges
        { 
            get 
            { 
                return this.m_OutEdges.AsReadOnly(); 
            }
        }

        internal void AddEmptyEdgeTo(NFAState targetstate)
        {
            CodeContract.RequireArgumentNotNull(targetstate, "targetstate");


            this.m_OutEdges.Add(new NFAEdge(targetstate));
        }

        internal void AddEdge(NFAEdge edge)
        {
            CodeContract.RequireArgumentNotNull(edge, "edge");

            m_OutEdges.Add(edge);
        }
    }
}
