namespace NScm.NativeProcedures
{
    using System;
    using System.Linq;

    internal static class Predicate
    {
        public static AtomBool IsBoolean(Atom x) => x is AtomBool;
        public static AtomBool IsNumber(Atom x) => x is AtomInt || x is AtomDouble;
        public static AtomBool IsString(Atom x) => x is AtomString;
        public static AtomBool IsSymbol(Atom x) => x is AtomSymbol;
        public static AtomBool IsList(Atom x) => x is AtomList;
        public static AtomBool IsNull(Atom x) => x is AtomList list && !list.Any();
        public static AtomBool IsEq(Atom x, Atom y) => x.Equals(y);
        public static AtomBool IsEqual(Atom x, Atom y) => EqualImpl(x, y);

        private static bool EqualImpl(Atom x, Atom y)
        {
            if (IsEq(x, y)) return true;
            if (x is AtomList xList && y is AtomList yList)
            {
                if (xList.Count != yList.Count)
                    return false;
                return Enumerable
                    .Zip(xList, yList, Tuple.Create)
                    .All(p => EqualImpl(p.Item1, p.Item2));
            }
            return false;
        }
    }
}
