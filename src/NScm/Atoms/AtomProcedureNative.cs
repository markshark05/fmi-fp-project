namespace NScm
{
    using System;

    public class AtomProcedureNative : Atom, IAtomProcedure
    {
        private readonly Func<AtomList, Atom> nativeFunc;

        private AtomProcedureNative(string name, Func<AtomList, Atom> func)
        {
            this.Name = name;
            this.nativeFunc = func;
        }

        public string Name { get; }

        public Atom Call(AtomList args)
        {
            return nativeFunc(args);
        }

        public override string ToString()
        {
            return string.Format("#<NativeProcedure:{0}>", Name);
        }

        public static AtomProcedureNative Create(string name, Func<Atom, Atom> func)
        {
            return new AtomProcedureNative(name, (args) =>
            {
                Syntax.AssertArity(args, 1);
                return func(args[0]);
            });
        }

        public static AtomProcedureNative Create(string name, Func<Atom, Atom, Atom> func)
        {
            return new AtomProcedureNative(name, (args) =>
            {
                Syntax.AssertArity(args, 2);
                return func(args[0], args[1]);
            });
        }

        public static AtomProcedureNative Create(string name, Func<AtomList, Atom> func)
        {
            return new AtomProcedureNative(name, func);
        }
    }
}
