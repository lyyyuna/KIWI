using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Compilers.Scanners.Generator;

namespace Compilers.Scanners
{
    public class TokenIdentity: IEquatable<TokenIdentity>
    {
        public int Index { get; private set; }
        public int IndexOfThisState { get; private set; }
        public Lexicon Lexicon { get; private set; }
        public LexerState State { get; private set; }
        public RegularExpression Definition { get; private set; }

        public TokenIdentity(RegularExpression definition, Lexicon lexicon, int index, LexerState state, int indexofthisstate)
        {
            Index = index;
            IndexOfThisState = indexofthisstate;
            Lexicon = lexicon;
            State = state;
            Definition = definition;
        }

        public bool Equals(TokenIdentity other)
        {
            if (other == null)
            {
                return false;
            }

            return (Lexicon == other.Lexicon) && (Index == other.Index);
        }


        // override
        public override bool Equals(object obj)
        {
            var other = obj as TokenIdentity;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Index;
        }

        public NFAModel CreateFiniteAutomationModel()
        {
            NFAModel nfa = NFAConverter.Default.Convert(Definition);

            // debug.assert

            // nfa.TailState.to

            return nfa;
        }
    }
}
