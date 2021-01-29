namespace NScm
{
    public static class Syntax
    {
        public static void Assert(bool success, Atom expr) => Assert(success, expr.ToString(), null);

        public static void AssertType<T>(Atom test) where T : Atom
        {
            if (!(test is T))
                throw new SyntaxException(string.Format("Conctract violation. Expected {0}, given {1}", typeof(T).Name, test.GetType().Name));
        }

        public static void AssertArity(AtomList args, int exact)
        {
            if (args.Count != exact)
                throw new SyntaxException(string.Format("Arity mismatch. Expected {0}, given {1}", exact, args.Count));
        }

        public static void AssertArityMin(AtomList args, int min)
        {
            if (args.Count < min)
                throw new SyntaxException(string.Format("Arity mismatch. Expected atleast {0}, given {1}", min, args.Count));
        }

        private static void Assert(bool success, string expr, string msg = null)
        {
            if (!success)
            {
                throw new SyntaxException(string.Format("{0}: {1}", msg ?? "Syntax error", expr));
            }
        }
    }
}
