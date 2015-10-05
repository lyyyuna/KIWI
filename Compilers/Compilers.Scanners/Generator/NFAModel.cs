using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Compilers.Scanners.Generator
{
    public class NFAModel
    {
        private List<NFAState> m_States;

        public NFAState TailState { get; internal set; }
        public NFAEdge EntryEdge { get; internal set; }

        internal NFAModel()
        {
            m_States = new List<NFAState>();
        }

        public ReadOnlyCollection<NFAState> States
        {
            get
            {
                return m_States.AsReadOnly();
            }
        }


        internal void AddState(NFAState state)
        {
            m_States.Add(state);
            state.Index = m_States.Count - 1;
        }

        internal void AddStates(IEnumerable<NFAState> states)
        {
            foreach (var s in states)
            {
                AddState(s);
            }
        }

    }
}
