namespace NScm.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class ListTests
    {
        [TestMethod]
        public void IsList() => AssertList("(list? ls)");
        [TestMethod]
        public void IsNotList() => AssertList("(not (list? 1))");
        [TestMethod]
        public void Length() => AssertList("(= 4 (length ls))");
        [TestMethod]
        public void LengthZero() => AssertList("(= 0 (length (list)))");
        [TestMethod]
        public void Car() => AssertList("(= (car ls) 1)");
        [TestMethod]
        public void CarCdr() => AssertList("(= (car (cdr ls)) 2)");
        [TestMethod]
        public void CarCdrCdr() => AssertList("(= (car (cdr (cdr ls))) 3)");
        [TestMethod]
        public void CarCdrCdrCdr() => AssertList("(= (car (cdr (cdr (cdr ls)))) 4)");
        [TestMethod]
        public void TestEquals() => AssertList("(equal? ls '(1 2 3 4))");
        [TestMethod]
        public void TestEq() => AssertList("(not (eq? '(1 2 3) '(1 2 3)))");
        [TestMethod]
        public void TestEqSame() => AssertList("(eq? ls ls)");
        [TestMethod]
        public void TestEqEmpty() => AssertList("(eq? '() '())");
        [TestMethod]
        public void TestNull() => AssertList("(null? (list))");
        [TestMethod]
        public void TestNotNull() => AssertList("(not (null? (list 1)))");

        private static void AssertList(string expr)
        {
            using var sr = new StringReader("(define ls (list 1 2 3 4))" + expr);
            var interp = new Interpreter();
            Atom res = interp.Evaluate(sr);
            Assert.IsTrue(res is AtomBool);
            Assert.IsTrue((AtomBool)res);
        }
    }
}
