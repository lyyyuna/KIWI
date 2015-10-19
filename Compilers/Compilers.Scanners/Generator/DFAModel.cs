using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using System.Diagnostics;

namespace Compilers.Scanners.Generator
{
    public class DFAModel
    {
        private NFAModel m_baseNFA;
        private HashSet<char> m_alphaset;
        private List<DFAState> m_states;
        private IComparer<char> m_charComparer;

        private DFAModel(NFAModel baseNFA)
        {
            m_baseNFA = baseNFA;
            m_alphaset = new HashSet<char>();
            m_states = new List<DFAState>();

            m_charComparer = Comparer<char>.Default;
        }


        public ReadOnlyCollection<DFAState> States
        {
            get
            {
                return m_states.AsReadOnly();
            }
        }

        private void AddState(DFAState state)
        {
            m_states.Add(state);
            state.Index = m_states.Count - 1;
        }

        // generate dfamodel
        public static DFAModel FromNFA(NFAModel nfa)
        {
            if (nfa == null) return null;

            var dfa = new DFAModel(nfa);
            var nfaStates = nfa.States;

            // get alphabet from all output edge symbols
            foreach(var s in nfaStates)
            {
                foreach (var edge in s.OutEdges)
                {
                    if (!edge.IsEmpty)
                    {
                        dfa.m_alphaset.Add(edge.Symbol.Value);
                    }
                }
            }

            // state0 is empty
            DFAState state0 = new DFAState();
            dfa.AddState(state0);

            // state1 is closure(nfastate[0])
            DFAState pre_state1 = new DFAState();
            int nfaStartIndex = nfa.EntryEdge.Targetstate.Index;

            Debug.Assert(nfaStartIndex >= 0);

            // add entry nfa nfastate[0]
            pre_state1.NFAStateSet.Add(nfaStartIndex);
            // get closure as dfa1
            DFAState state1 = dfa.GetClosure(pre_state1);
            dfa.AddState(state1);


            // begin algorithm
            int p = 1, j = 0;
            while (j<=p)
            {
                foreach (var symbol in dfa.m_alphaset)
                {
                    DFAState e = dfa.GetDFAState(dfa.m_states[j], symbol);

                    bool isSetExit = false;
                    for (int i = 0; i<=p; i++)
                    {
                        if (e.NFAStateSet.SetEquals(dfa.m_states[i].NFAStateSet))
                        {
                            DFAEdge newEdge = new DFAEdge(symbol, dfa.m_states[i]);
                            dfa.m_states[j].AddEdge(newEdge);
                            isSetExit = true;
                        }

                    }

                    if (isSetExit == false)
                    {
                        p++;
                        dfa.AddState(e);

                        DFAEdge newEdge = new DFAEdge(symbol, e);
                        dfa.m_states[j].AddEdge(newEdge);
                    }
                }

                j++;
            }

            return dfa;
        }

        private DFAState GetDFAState(DFAState start, char symbol)
        {
            DFAState target = new DFAState();
            var nfastates = m_baseNFA.States;

            foreach (var nfaindex in start.NFAStateSet)
            {
                NFAState nfastate = nfastates[nfaindex];
                foreach (var edge in nfastate.OutEdges)
                {
                    if (!edge.IsEmpty && m_charComparer.Compare(symbol, edge.Symbol.Value) == 0)
                    {
                        int targetindex = edge.Targetstate.Index;

                        target.NFAStateSet.Add(targetindex);
                    }
                }
            }

            return GetClosure(target);
        }

        private DFAState GetClosure(DFAState state)
        {
            var closure = new DFAState();

            var nfaStates = m_baseNFA.States;

            closure.NFAStateSet.UnionWith(state.NFAStateSet);

            var changedFlag = true;
            while (changedFlag)
            {
                changedFlag = false;

                var lastStateSet = closure.NFAStateSet.ToList();
                foreach (var nfaindex in lastStateSet)
                {
                    NFAState nfastate = nfaStates[nfaindex];
                    foreach (var edge in nfastate.OutEdges)
                    {
                        // empty edge
                        if (edge.IsEmpty)
                        {
                            var targetstate = edge.Targetstate;
                            // judge if closure changed
                            changedFlag = closure.NFAStateSet.Add(targetstate.Index) || changedFlag;
                        }
                    }
                }
            }

            return closure;
        }

    }
}
