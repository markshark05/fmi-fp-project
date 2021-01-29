namespace NScm.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void LamdaTest()
        {
            using var sr = new StringReader(
                "(define ++ (lambda (x) (+ x 1)))" +
                "(define (f op n) (op n))" +
                "(= (f ++ 5) 6)"
            );
            var interp = new Interpreter();
            Atom res = interp.Evaluate(sr);
            Assert.IsTrue(res is AtomBool);
            Assert.IsTrue((AtomBool)res);
        }

        [TestMethod]
        public void FactorialTest()
        {
            using var sr = new StringReader(
                "(define factorial" +
                    "(lambda(n)" +
                        "(if (= n 0)" +
                        "1" +
                        "(* n (factorial (- n 1))))))" +
                "(= (factorial 5) 120)"
            );
            var interp = new Interpreter();
            Atom res = interp.Evaluate(sr);
            Assert.IsTrue(res is AtomBool);
            Assert.IsTrue((AtomBool)res);
        }
    }
}
