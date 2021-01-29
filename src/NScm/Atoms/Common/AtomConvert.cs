namespace NScm
{
    using System;

    public static class AtomConvert
    {
        public static IAtomProcedure ToProcedure(Atom a)
        {
            switch (a)
            {
                case IAtomProcedure proc: return proc;
                default: throw new SyntaxException("Expected procedure.");
            }
        }

        public static AtomList ToList(Atom a)
        {
            switch (a)
            {
                case AtomList aList: return aList;
                default: throw new SyntaxException("Expected list.");
            }
        }

        public static AtomDouble ToDouble(Atom a)
        {
            switch (a)
            {
                case AtomDouble aDouble: return aDouble;
                case AtomInt aInt: return Convert.ToDouble(aInt);
                default: throw new SyntaxException("Expected numeric type.");
            }
        }

        public static AtomInt ToInt(Atom a)
        {
            switch (a)
            {
                case AtomInt aInt: return aInt;
                case AtomDouble aDouble: return Convert.ToInt32(aDouble);
                default: throw new SyntaxException("Expected numeric type.");
            }
        }
    }
}
