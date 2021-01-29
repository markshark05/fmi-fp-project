namespace NScm
{
    public class AtomDouble : AtomPrimitive<double>, IAtomPrimitive<double>
    {
        private AtomDouble(double value) : base(value)
        {
        }

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is AtomDouble d) return Equals(d);
            if (obj is AtomInt i) return NativeValue.Equals(i.NativeValue);
            return false;
        }

        public static bool TryParse(string s, out AtomDouble result)
        {
            if (int.TryParse(s, out int intRes))
            {
                result = new AtomDouble(intRes);
                return true;
            }
            result = new AtomDouble(0.0D);
            return false;
        }

        public static implicit operator AtomDouble(double d) => new AtomDouble(d);
    }
}
