namespace NScm
{
    using System;
    using System.Collections.Generic;

    public class AtomSymbol : Atom, IEquatable<AtomSymbol>
    {
        private static readonly IDictionary<string, AtomSymbol> cache = new Dictionary<string, AtomSymbol>();
        private readonly string RawValue;

        public static readonly AtomSymbol IF = FromString("if");
        public static readonly AtomSymbol AND = FromString("and");
        public static readonly AtomSymbol OR = FromString("or");
        public static readonly AtomSymbol QUOTE = FromString("quote");
        public static readonly AtomSymbol DEFINE = FromString("define");
        public static readonly AtomSymbol LAMBDA = FromString("lambda");
        public static readonly AtomSymbol BEGIN = FromString("begin");
        public static readonly AtomSymbol EOF = FromString("#<eof>");

        private AtomSymbol(string sym)
        {
            RawValue = sym;
        }

        public bool Equals(Atom other)
        {
            switch (other)
            {
                case AtomSymbol s: return Equals(s);
                default: return false;
            }
        }

        public bool Equals(AtomSymbol other)
        {
            if (other is null) return false;
            if (GetType() != other.GetType()) return false;
            return this.RawValue == other.RawValue;
        }

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case AtomSymbol s: return Equals(s);
                default: return false;
            }
        }

        public override string ToString()
        {
            return string.Format("'{0}", RawValue);
        }

        public static AtomSymbol FromString(string sym)
        {
            if (!cache.TryGetValue(sym, out _))
            {
                cache[sym] = new AtomSymbol(sym);
            }

            return cache[sym];
        }

        public override int GetHashCode()
        {
            return RawValue.GetHashCode();
        }

        public static bool operator ==(AtomSymbol left, AtomSymbol right) => EqualityComparer<AtomSymbol>.Default.Equals(left, right);
        public static bool operator !=(AtomSymbol left, AtomSymbol right) => !(left == right);
    }
}
