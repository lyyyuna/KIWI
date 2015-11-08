using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public class LexerBuilder
    {
        private Lexicon m_lexicon;
        private DFAModel m_dfa;
        private NFAModel m_nfa;

        private HashSet<char> m_alphabet;
        private List<DFAState> m_dfaStates;
        private IComparer<char> m_charComparer;

        public LexerBuilder(Lexicon lexicon)
        {
            CodeContract.RequireArgumentNotNull(lexicon, "lexicon");

            m_lexicon = lexicon;

            m_dfaStates = new List<DFAState>();
            m_alphabet = new HashSet<char>();

            m_charComparer = Comparer<char>.Default;

        }

        public NFAModel ConvertLexiconToNFA()
        {
            NFAModel lexerNFA = new NFAModel();
            NFAState entryState = new NFAState();

            lexerNFA.AddState(entryState);
            foreach (var token in m_lexicon.GetTokens())
            {
                NFAModel tokenNFA = token.CreateFiniteAutomationModel();

                entryState.AddEdge(tokenNFA.EntryEdge);
                lexerNFA.AddStates(tokenNFA.States);
            }

            lexerNFA.EntryEdge = new NFAEdge(entryState);

            return lexerNFA;
        }

        public DFAModel ConvertNFAToDFA()
        {
            if (m_nfa == null)
                return null;

            var nfastates = m_nfa.States;

            // get alphabet
            foreach (var s in nfastates)
            {
                foreach (var edge in s.OutEdges)
                {
                    if(!edge.IsEmpty)
                    {
                        m_alphabet.Add(edge.Symbol.Value);
                    }
                }
            }

            // state0 is empty
            DFAState state0 = new DFAState();
            AddDFAState(state0);

            // state1 is closure(nfa0)
            DFAState prestate1 = new DFAState();
            int entrynfaIndex = m_nfa.EntryEdge.Targetstate.Index;

            prestate1.NFAStateSet.Add(entrynfaIndex);

            DFAState state1 = GetClosure(prestate1);
            AddDFAState(state1);

            // begin algorithm
            int p = 1, j = 0;
            while (j <= p)
            {
                foreach(var symbol in m_alphabet)
                {
                    DFAState e = GetDFAState(m_dfaStates[j], symbol);

                    bool isSetExisted = false;
                    for (int i=0; i<=p; i++)
                    {
                        if(e.NFAStateSet.SetEquals(m_dfaStates[i].NFAStateSet))
                        {
                            // exist
                            DFAEdge newedge = new DFAEdge(symbol, m_dfaStates[i]);
                            m_dfaStates[j].AddEdge(newedge);

                            isSetExisted = true;
                        }
                    }

                    if (!isSetExisted)
                    {
                        p += 1;
                        AddDFAState(e);

                        DFAEdge newedge = new DFAEdge(symbol, e);
                        m_dfaStates[j].AddEdge(newedge);
                    }
                }

                j += 1;
            }

            return m_dfa;
        }

        public void AddDFAState(DFAState state)
        {
            m_dfaStates.Add(state);
            state.Index = m_dfaStates.Count - 1;
            
        }

        public DFAState GetClosure(DFAState state)
        {
            DFAState closure = new DFAState();

            var nfastates = m_nfa.States;
            closure.NFAStateSet.UnionWith(state.NFAStateSet);

            bool changed = true;
            while (changed == true)
            {
                changed = false;
                List<int> lastStateSets = closure.NFAStateSet.ToList();

                foreach (var stateIndex in lastStateSets)
                {
                    NFAState s = nfastates[stateIndex];
                    foreach (var edge in s.OutEdges)
                    {
                        if (edge.IsEmpty)
                        {
                            NFAState target = edge.Targetstate;
                            int targetIndex = target.Index;

                            changed = closure.NFAStateSet.Add(targetIndex) || changed;
                        }
                    }
                }
            }

            return closure;
        }

        private DFAState GetDFAState(DFAState start, char symbol)
        {
            DFAState targetDFA = new DFAState();
            var nfastates = m_nfa.States;

            foreach (var nfastatesIndex in start.NFAStateSet)
            {
                NFAState nfaState = nfastates[nfastatesIndex];

                foreach (var edge in nfaState.OutEdges)
                {
                    if (!edge.IsEmpty && m_charComparer.Compare(symbol, edge.Symbol.Value)==0)
                    {
                        int targetIndex = edge.Targetstate.Index;

                        targetDFA.NFAStateSet.Add(targetIndex);
                    }
                }
            }

            return GetClosure(targetDFA);
        }
       
    }
}
