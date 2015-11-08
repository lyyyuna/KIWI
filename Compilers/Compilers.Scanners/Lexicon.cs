using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilers.Scanners.Generator;
using System.Collections.ObjectModel;

namespace Compilers.Scanners
{
    public class Lexicon
    {
        private List<TokenIdentity> m_tokens;
        private readonly LexerState m_defaultState;
        private List<LexerState> m_lexerStates;

        public Lexicon()
        {
            m_tokens = new List<TokenIdentity>();
            m_lexerStates = new List<LexerState>();
            m_defaultState = new LexerState(this, 0);

            m_lexerStates.Add(m_defaultState);
        }

        public LexerState DefaultLexState
        {
            get
            {
                return m_defaultState;
            }
        }

        public TokenIdentity AddToken(RegularExpression definition, LexerState state, int IndexOfThisState)
        {
            int index = m_tokens.Count;
            TokenIdentity token = new TokenIdentity(definition, this, index, state, IndexOfThisState);
            m_tokens.Add(token);

            return token;
        }

        public ReadOnlyCollection<TokenIdentity>  GetTokens()
        {
            return m_tokens.AsReadOnly();
        }

        public LexerState DefineState()
        {
            int index = m_lexerStates.Count;
            LexerState newsState = new LexerState(this, index);
            m_lexerStates.Add(newsState);

            return newsState;

        }

        public LexerState DefineState(LexerState baselexerstate)
        {
            int index = m_lexerStates.Count;
            LexerState newState = new LexerState(this, index, baselexerstate);
            m_lexerStates.Add(newState);

            return newState;
        }

        public NFAModel CreateFiniteAutomationModel()
        {
            NFAModel lexerNFA = new NFAModel();
            NFAState entryState = new NFAState();

            lexerNFA.AddState(entryState);
            foreach (var token in m_tokens)
            {
                NFAModel tokenNFA = token.CreateFiniteAutomationModel();

                entryState.AddEdge(tokenNFA.EntryEdge);
                lexerNFA.AddStates(tokenNFA.States);
            }

            lexerNFA.EntryEdge = new NFAEdge(entryState);


            return lexerNFA;
        }
    }
}
