namespace NScm
{
    public class AtomString : AtomPrimitive<string>, IAtomPrimitive<string>
    {
        public AtomString(string value) : base(value ?? string.Empty)
        {
        }

        public override string ToString()
        {
            return string.Format("\"{0}\"", NativeValue);
        }

        public static implicit operator AtomString(string s) => new AtomString(s);
    }
}
