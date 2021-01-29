namespace NScm
{
    public class AtomVoid : Atom
    {
        public static readonly AtomVoid Instance = new AtomVoid();

        public override string ToString() => "#<void>";
    }
}
