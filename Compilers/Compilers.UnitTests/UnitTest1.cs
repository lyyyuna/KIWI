using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Compilers.Scanners;
using Compilers.Scanners.Generator;
using RE = Compilers.Scanners.RegularExpression;

namespace Compilers.UnitTests
{
    [TestClass]
    public class ScannerTest
    {
        [TestMethod]
        public void RegExToDFATest()
        {
            var RE_ID = RE.Range('a', 'z').Concat((RE.Range('a', 'z') | RE.Range('0', '9')).Many());

            NFAConverter nfa_converter = new NFAConverter();
            NFAModel N_ID = nfa_converter.Convert(RE_ID);
            DFAModel D_ID = DFAModel.FromNFA(N_ID);

            // verify state0
            var state0 = D_ID.States[0];
            // state0 is empty, every wrong input goes to it 
            Assert.AreEqual(36, state0.OutEdges.Count);
            foreach (var edge in state0.OutEdges)
            {
                Assert.AreEqual(0, edge.TargetState.Index);
            }

            // verify initial state
            var state1 = D_ID.States[1];

            foreach (var edge in state1.OutEdges)
            {
                if (edge.Symbol >='a' && edge.Symbol <='z')
                {
                    Assert.IsTrue(edge.TargetState.Index > 0);
                }
                else
                {
                    Assert.AreEqual(0, edge.TargetState.Index);
                }
            }
        }
    }
}
