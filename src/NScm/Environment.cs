namespace NScm
{
    using System;
    using System.Collections.Generic;

    public class Environment
    {
        private readonly IDictionary<AtomSymbol, Atom> symbols;
        private readonly Environment parent;

        public Environment(IDictionary<AtomSymbol, Atom> env, Environment outer)
        {
            this.symbols = env ?? throw new ArgumentNullException(nameof(env));
            this.parent = outer;
        }

        public static Environment CreateEmpty()
        {
            return new Environment(new Dictionary<AtomSymbol, Atom>(), null);
        }

        public Environment CreateChild(Atom parameters, AtomList values)
        {
            if (parameters is AtomSymbol parametersSymbol)
            {
                return new Environment(new Dictionary<AtomSymbol, Atom>() { { parametersSymbol, values } }, this);
            }

            var args = (AtomList)parameters;
            Syntax.AssertArity(args, values.Count);

            var dict = new Dictionary<AtomSymbol, Atom>();
            for (int i = 0; i < values.Count; i++)
            {
                dict[(AtomSymbol)args[i]] = values[i];
            }

            return new Environment(dict, this);
        }

        public Atom this[AtomSymbol sym]
        {
            get
            {
                Atom val;
                if (TryGetValue(sym, out val))
                {
                    return val;
                }
                throw new SyntaxException(string.Format("{0}: undefined", sym));
            }
            set
            {
                symbols[sym] = value;
            }
        }

        private bool TryGetValue(AtomSymbol sym, out Atom val)
        {
            Environment env = FindEnclosing(sym);
            if (env != null)
            {
                val = env.symbols[sym];
                return true;
            }
            val = default;
            return false;
        }

        private Environment FindEnclosing(AtomSymbol sym)
        {
            if (symbols.TryGetValue(sym, out _))
            {
                return this;
            }
            return parent?.FindEnclosing(sym);
        }
    }
}
