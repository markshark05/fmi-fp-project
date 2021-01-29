namespace NScm
{
    public class AtomInt : AtomPrimitive<int>, IAtomPrimitive<int>
    {
        private AtomInt(int value) : base(value)
        {
        }

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is AtomInt i) return Equals(i);
            if (obj is AtomDouble d) return NativeValue.Equals(d.NativeValue);
            return false;
        }

        public static bool TryParse(string s, out AtomInt result)
        {
            if (int.TryParse(s, out int intRes))
            {
                result = new AtomInt(intRes);
                return true;
            }
            result = new AtomInt(0);
            return false;
        }

        public static implicit operator AtomInt(int i) => new AtomInt(i);
    }
}
