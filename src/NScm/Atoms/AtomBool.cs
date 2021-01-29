namespace NScm
{
    public class AtomBool : AtomPrimitive<bool>, IAtomPrimitive<bool>
    {
        public AtomBool(bool value) : base(value)
        {
        }
        
        public override string ToString()
        {
            return NativeValue ? "#t" : "#f";
        }

        public static AtomBool True = new AtomBool(true);
        public static AtomBool False = new AtomBool(false);

        public static implicit operator AtomBool(bool b) => b ? True : False;
    }
}
