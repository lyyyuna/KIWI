using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public class NFAConverter: RegularExpressionConvertor<NFAModel>
    {
        public override NFAModel ConvertAlternation(AlternationExpression exp)
        {
            var nfa1 = Convert(exp.Expression1);
            var nfa2 = Convert(exp.Expression2);

            NFAState head = new NFAState();
            NFAState tail = new NFAState();

            head.AddEdge(nfa1.EntryEdge);
            head.AddEdge(nfa2.EntryEdge);

            nfa1.TailState.AddEmptyEdgeTo(tail);
            nfa2.TailState.AddEmptyEdgeTo(tail);

            var alternationNfa = new NFAModel();

            alternationNfa.AddState(head);
            alternationNfa.AddStates(nfa1.States);
            alternationNfa.AddStates(nfa2.States);
            alternationNfa.AddState(tail);

            alternationNfa.EntryEdge = new NFAEdge(head);
            alternationNfa.TailState = tail;

            return alternationNfa;
        }

        public override NFAModel ConvertSymbol(SymbolExpression exp)
        {
            var tail = new NFAState();
            var entryEdge = new NFAEdge(exp.Symbol, tail);

            var symbolNfa = new NFAModel();

            symbolNfa.AddState(tail);
            symbolNfa.TailState = tail;
            symbolNfa.EntryEdge = entryEdge;

            return symbolNfa;

        }

        public override NFAModel ConvertEmpty(EmptyExpression exp)
        {
            var tail = new NFAState();
            var entryEdge = new NFAEdge(tail);

            var emptyNfa = new NFAModel();

            emptyNfa.AddState(tail);
            emptyNfa.TailState = tail;
            emptyNfa.EntryEdge = entryEdge;

            return emptyNfa;
        }

        public override NFAModel ConvertConcatenation(ConcatenationExpression exp)
        {
            var leftNfa = Convert(exp.Left);
            var rightNfa = Convert(exp.Right);

            leftNfa.TailState.AddEdge(rightNfa.EntryEdge);

            var concatenationNfa = new NFAModel();

            concatenationNfa.AddStates(leftNfa.States);
            concatenationNfa.AddStates(rightNfa.States);
            concatenationNfa.EntryEdge = leftNfa.EntryEdge;
            concatenationNfa.TailState = rightNfa.TailState;

            return concatenationNfa;
        }

        public override NFAModel ConvertAlternationCharSet(AlternationCharSetExpression exp)
        {
            var head = new NFAState();
            var tail = new NFAState();

            var alternationcharsetNfa = new NFAModel();

            alternationcharsetNfa.AddState(head);

            foreach(var symbol in exp.CharSet)
            {
                var symbolEdge = new NFAEdge(symbol, tail);
                head.AddEdge(symbolEdge);
            }

            alternationcharsetNfa.AddState(tail);

            // add empty entry edge
            alternationcharsetNfa.EntryEdge = new NFAEdge(head);
            alternationcharsetNfa.TailState = tail;

            return alternationcharsetNfa;

        }

        public override NFAModel ConvertStringLiteral(StringLiteralExpression exp)
        {
            var literalNfa = new NFAModel();

            NFAState lastState = null;

            foreach(var symbol in exp.Literal)
            {
                var symbolState = new NFAState();
                var symbolEdge = new NFAEdge(symbol, symbolState);

                if (lastState == null)
                {
                    literalNfa.EntryEdge = symbolEdge;
                }
                else
                {
                    lastState.AddEdge(symbolEdge);
                }

                lastState = symbolState;
                literalNfa.AddState(symbolState);
            }

            literalNfa.TailState = lastState;

            return literalNfa;

        }


        public override NFAModel ConvertKleeneStar(KleeneStarExpression exp)
        {
            var innerNfa = Convert(exp.InnerExpression);

            var tail = new NFAState();
            var entry = new NFAEdge(tail);

            innerNfa.TailState.AddEmptyEdgeTo(tail);
            tail.AddEdge(innerNfa.EntryEdge);

            var kleenstarNfa = new NFAModel();

            kleenstarNfa.AddStates(innerNfa.States);
            kleenstarNfa.AddState(tail);
            kleenstarNfa.EntryEdge = entry;
            kleenstarNfa.TailState = tail;

            return kleenstarNfa;
        }
    }
}
