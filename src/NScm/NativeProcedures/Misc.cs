namespace NScm.NativeProcedures
{
    internal static class Misc
    {
        public static AtomVoid Void(AtomList args) => AtomVoid.Instance;
        public static AtomBool Not(Atom x) => x is AtomBool xBool && !xBool;
    }
}
