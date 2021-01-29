namespace NScm.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void TestEmpty() => AssertEqual("(map + '())", "'()");
        [TestMethod]
        public void TestSingleList() => AssertEqual("(map + '(1 2 3))", "'(1 2 3)");
        [TestMethod]
        public void TestDoubleList() => AssertEqual("(map + '(1 2 3) '(1 2 3))", "'(2 4 6)");
        [TestMethod]
        public void TestUseDefinedFn() => AssertEqual(
            "(begin" +
                "(define (func a b c) (+ (* a b) c))" +
                "(map func '(1 2 3) '(4 5 6) '(7 8 9))" +
            ")",
            "'(11 18 27)"
        );

        private static void AssertEqual(string test, string expected)
        {
            var interpreter = new Interpreter();
            using var reader = new StringReader(string.Format("(equal? {0} {1})", test, expected));
            Atom res = interpreter.Evaluate(reader);
            Assert.IsTrue(res is AtomBool);
            Assert.IsTrue((AtomBool)res);
        }
    }
}
