using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public class LexerState
    {
        private List<TokenIdentity> m_tokens;
        public Lexicon Lexicon { get; private set; }
        public LexerState BaseLexerState { get; private set; }
        public int Index { get; private set; }
        public int level { get; private set; }

        public LexerState(Lexicon lexicon, int index):this(lexicon, index, null)
        {

        }

        public LexerState(Lexicon lexicon, int index, LexerState baselexerstate)
        {
            Lexicon = lexicon;
            BaseLexerState = baselexerstate;
            Index = index;
            m_tokens = new List<TokenIdentity>();

            if (BaseLexerState == null)
            {
                level = 0;
            }
            else
            {
                level = BaseLexerState.level + 1;
            }
        }

        public TokenIdentity DefineToken(RegularExpression regex)
        {
            CodeContract.RequireArgumentNotNull(regex, "regex");

            int IndexOfThisState = m_tokens.Count;
            var token = Lexicon.AddToken(regex, this, IndexOfThisState);
            m_tokens.Add(token);

            return token;
        }
    }
}
