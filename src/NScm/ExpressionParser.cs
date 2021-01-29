namespace NScm
{
    using System;
    using System.Linq;

    internal static class ExpressionParser
    {
        public static Atom Evaluate(Atom expr, Environment env)
        {
            while (true)
            {
                if (expr is AtomSymbol symbol)
                {
                    return env[symbol];
                }
                if (!(expr is AtomList exprList))
                {
                    return expr;
                }
                if (AtomSymbol.QUOTE.Equals(exprList[0]))
                {
                    return exprList[1];
                }
                if (AtomSymbol.IF.Equals(exprList[0]))
                {
                    Atom conditionBody = exprList[1];
                    Atom ifBody = exprList[2];
                    Atom elseBody = exprList[3];
                    Atom ifStatement = Evaluate(conditionBody, env);
                    expr = (!(ifStatement is AtomBool b) || b) ? ifBody : elseBody;
                }
                else if (AtomSymbol.AND.Equals(exprList[0]))
                {
                    return AND_OR(a => Evaluate(a, env), exprList, true, isFalse => isFalse);
                }
                else if (AtomSymbol.OR.Equals(exprList[0]))
                {
                    return AND_OR(a => Evaluate(a, env), exprList, false, isFalse => !isFalse);
                }
                else if (AtomSymbol.DEFINE.Equals(exprList[0]))
                {
                    var variable = (AtomSymbol)exprList[1];
                    expr = exprList[2];
                    env[variable] = Evaluate(expr, env);
                    return AtomVoid.Instance;
                }
                else if (AtomSymbol.LAMBDA.Equals(exprList[0]))
                {
                    Atom parameters;
                    if (exprList[1] is AtomSymbol lambdaSymbol)
                        parameters = lambdaSymbol;
                    else
                        parameters = ((AtomList)exprList[1]).Cast<AtomSymbol>().ToAtomList();

                    return new AtomProcedure(parameters, exprList[2], env);
                }
                else if (AtomSymbol.BEGIN.Equals(exprList[0]))
                {
                    for (int i = 1; i < exprList.Count - 1; i++)
                    {
                        Evaluate(exprList[i], env);
                    }
                    expr = exprList[exprList.Count - 1];
                }
                else
                {
                    Atom rawProc = Evaluate(exprList[0], env);
                    AtomList args = exprList.Skip(1).Select(a => Evaluate(a, env)).ToAtomList();
                    if (rawProc is IAtomProcedure proc)
                    {
                        return proc.Call(args);
                    }
                    else
                    {
                        throw new SyntaxException(string.Format("{0}: not a precedure", rawProc));
                    }
                }
            }
        }

        private static Atom AND_OR(Func<Atom, Atom> eval, AtomList exprList, AtomBool init, Func<bool, bool> breakCond)
        {
            Atom result = init;
            for (int i = 1; i < exprList.Count; i++)
            {
                result = eval(exprList[i]);
                if (breakCond(result is AtomBool b && !b))
                {
                    break;
                }
            }
            return result;
        }
    }
}
