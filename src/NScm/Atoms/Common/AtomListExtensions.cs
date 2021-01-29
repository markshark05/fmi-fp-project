namespace NScm
{
    using System.Collections.Generic;

    public static class AtomListExtensions
    {
        public static AtomList ToAtomList(this IEnumerable<Atom> atoms)
        {
            return new AtomList(atoms);
        }
    }
}
