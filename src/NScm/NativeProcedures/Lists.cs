namespace NScm.NativeProcedures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Lists
    {
        public static AtomList List(AtomList args) => args;
        public static Atom Reverse(Atom arg) => AtomConvert.ToList(arg).Reverse().ToAtomList();
        public static AtomInt Length(Atom x) => AtomConvert.ToList(x).Count;
        public static Atom Car(Atom x) => AtomConvert.ToList(x)[0];
        public static AtomList Cdr(Atom x) => AtomConvert.ToList(x).Skip(1).ToAtomList();
        public static Atom Apply(Atom x, Atom y) => AtomConvert.ToProcedure(x).Call(AtomConvert.ToList(y));
        public static AtomList Append(AtomList args)
        {
            if (!args.Any()) return new AtomList();
            return args
                .Take(args.Count - 1)
                .SelectMany(x => (AtomList)x)
                .Concat(new[] { args.Last() })
                .ToAtomList();
        }
        public static AtomList Map(AtomList args)
        {
            Syntax.AssertArityMin(args, 2);
            IAtomProcedure proc = AtomConvert.ToProcedure(args[0]);
            IEnumerable<AtomList> lists = args.Skip(1).Select(AtomConvert.ToList);

            if (Enumerable
                .Zip(lists, lists.Skip(1), Tuple.Create)
                .Any(p => p.Item1.Count != p.Item2.Count))
            {
                throw new SyntaxException("All lists must have the same size");
            }

            return Enumerable
                .Aggregate(
                    lists,
                    lists.First().Select(x => new AtomList()),
                    (acc, src) => Enumerable
                        .Zip(acc, src, (a, b) => a
                            .Concat(new[] { b }).ToAtomList()))
                .Select(x => proc.Call(x))
                .ToAtomList();
        }
    }
}
