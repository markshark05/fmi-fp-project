namespace NScm.NativeProcedures
{
    using System.Collections.Generic;
    using System.Linq;

    using static AtomProcedureNative;

    internal static class NativeProcedureBuilder
    {
        public static IDictionary<AtomSymbol, Atom> Build()
        {
            return new AtomProcedureNative[]
            {
                Create("+", Numeric.Add),
                Create("-", Numeric.Minus),
                Create("*", Numeric.Multiply),
                Create("/", Numeric.Divide),
                Create("=", Numeric.Eq),
                Create("<", Numeric.Lt),
                Create("<=", Numeric.Lte),
                Create(">", Numeric.Gt),
                Create(">=", Numeric.Gte),
                Create("expt", Numeric.Expt),
                Create("remainder", Numeric.Remainder),
                Create("quotient", Numeric.Quotient),

                Create("eq?", Predicate.IsEq),
                Create("eqv?", Predicate.IsEq),
                Create("equal?", Predicate.IsEqual),
                Create("boolean?", Predicate.IsBoolean),
                Create("number?", Predicate.IsNumber),
                Create("string?", Predicate.IsString),
                Create("symbol?", Predicate.IsSymbol),
                Create("null?", Predicate.IsNull),
                Create("list?", Predicate.IsList),

                Create("list", Lists.List),
                Create("car", Lists.Car),
                Create("cdr", Lists.Cdr),
                Create("length", Lists.Length),
                Create("append", Lists.Append),
                Create("reverse", Lists.Reverse),
                Create("map", Lists.Map),
                Create("apply", Lists.Apply),

                Create("not", Misc.Not),
                Create("void", Misc.Void),
            }
            .ToDictionary(proc => AtomSymbol.FromString(proc.Name), proc => (Atom)proc);
        }
    }
}
