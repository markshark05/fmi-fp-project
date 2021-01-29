namespace NScm.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class ArithmeticTests
    {
        [TestMethod]
        public void TestPlus() => AssertEqual("(+ 1 2)", "3");
        [TestMethod]
        public void TestPlusParams() => AssertEqual("(+ 1 2 3 4 5)", "15");
        [TestMethod]
        public void TestMinus() => AssertEqual("(- 2 1)", "1");
        [TestMethod]
        public void TestMultiply() => AssertEqual("(* 2 3)", "6");
        [TestMethod]
        public void TestDivide() => AssertEqual("(/ 4 3)", "1");
        [TestMethod]
        public void TestEql1() => AssertEqual("(= 1 1)", "#t");
        [TestMethod]
        public void TestEql2() => AssertEqual("(= 1 2)", "#f");
        [TestMethod]
        public void TestLt() => AssertEqual("(< 1 2)", "#t");
        [TestMethod]
        public void TestGt() => AssertEqual("(> 1 2)", "#f");
        [TestMethod]
        public void TestGtMany() => AssertEqual("(> 6 5 4 3)", "#t");
        [TestMethod]
        public void TestGtManyFalse() => AssertEqual("(> 6 5 6 3)", "#f");
        [TestMethod]
        public void TestGtEmpty() => AssertEqual("(>)", "#t");

        private static void AssertEqual(string test, string expected)
        {
            var interpreter = new Interpreter();
            using var reader = new StringReader(string.Format("(eq? {0} {1})", test, expected));
            Atom res = interpreter.Evaluate(reader);
            Assert.IsTrue(res is AtomBool);
            Assert.IsTrue((AtomBool)res);
        }
    }
}
