namespace NScm.NativeProcedures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Numeric
    {
        public static AtomBool Eq(AtomList list) => CompareAll(list, c => c == 0);
        public static AtomBool Lt(AtomList list) => CompareAll(list, c => c < 0);
        public static AtomBool Gt(AtomList list) => CompareAll(list, c => c > 0);
        public static AtomBool Lte(AtomList list) => CompareAll(list, c => c <= 0);
        public static AtomBool Gte(AtomList list) => CompareAll(list, c => c >= 0);
        public static Atom Add(AtomList list) => Aggregate(list, (AtomInt)0, (a, b) => a + b, (a, b) => a + b);
        public static Atom Multiply(AtomList list) => Aggregate(list, (AtomInt)1, (a, b) => a * b, (a, b) => a * b);
        public static Atom Minus(AtomList list) => Aggregate(list, (a, b) => a - b, (a, b) => a - b);
        public static Atom Divide(AtomList list) => Aggregate(list, (a, b) => a / b, (a, b) => a / b);
        public static Atom Expt(Atom x, Atom y) => NumericResult(x, y, (a, b) => Convert.ToInt32(Math.Pow(a, b)), (a, b) => Math.Pow(a, b));
        public static Atom Remainder(Atom x, Atom y) => IntegerResult(x, y, (a, b) => a % b);
        public static Atom Quotient(Atom x, Atom y) => IntegerResult(x, y, (a, b) => a / b);

        private static AtomBool CompareAll(AtomList list, Func<int, bool> cmp)
        {
            return Enumerable
                .Zip(list, list.Skip(1), Tuple.Create)
                .All(p => cmp(CompareNumeric(p.Item1, p.Item2)));
        }
        private static int CompareNumeric(Atom x, Atom y)
        {
            if (x is AtomInt xInt && y is AtomInt yInt)
                return Comparer<AtomInt>.Default.Compare(xInt, yInt);
            return Comparer<AtomDouble>.Default.Compare(AtomConvert.ToDouble(x), AtomConvert.ToDouble(y));
        }
        private static Atom Aggregate(AtomList list, Atom seed, Func<int, int, int> intFunc, Func<double, double, double> doubleFunc)
        {
            return list.Aggregate(seed, (x, y) => NumericResult(x, y, intFunc, doubleFunc));
        }
        private static Atom Aggregate(AtomList list, Func<int, int, int> intFunc, Func<double, double, double> doubleFunc)
        {
            Syntax.AssertArityMin(list, 1);
            return list.Aggregate((x, y) => NumericResult(x, y, intFunc, doubleFunc));
        }
        private static Atom IntegerResult(Atom x, Atom y, Func<int, int, int> intFunc)
        {
            return NumericResult(x, y, intFunc, (a, b) => throw new SyntaxException("Expected integer type."));
        }
        private static Atom NumericResult(Atom x, Atom y, Func<int, int, int> intFunc, Func<double, double, double> doubleFunc)
        {
            if (x is AtomInt xInt && y is AtomInt yInt)
                return (AtomInt)intFunc(xInt, yInt);
            return (AtomDouble)doubleFunc(AtomConvert.ToDouble(x), AtomConvert.ToDouble(y));
        }
    }
}
