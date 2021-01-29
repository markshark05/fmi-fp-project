namespace NScm.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class SyntaxTest
    {
        [TestMethod]
        public void Empty() => Evaluate("");
        [TestMethod]
        public void DefineLiteral() => Evaluate("(define x 1)");
        [TestMethod]
        public void DefineProc() => Evaluate("(define (x) 1)");
        [TestMethod]
        public void DefineError1() => EvaluateWithError("(define)");
        [TestMethod]
        public void DefineError2() => EvaluateWithError("(define x)");
        [TestMethod]
        public void EmptyError() => EvaluateWithError("()");

        private static void Evaluate(string str)
        {
            EvaluateImpl(str);
        }

        private static void EvaluateWithError(string str)
        {
            Assert.ThrowsException<SyntaxException>(() => EvaluateImpl(str));
        }

        private static Atom EvaluateImpl(string str)
        {
            var interpreter = new Interpreter();
            using var reader = new StringReader(str);
            return interpreter.Evaluate(reader);
        }
    }
}
