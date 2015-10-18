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
            Assert.AreEqual(36, state0.OutEdges.Count);

        }
    }
}
