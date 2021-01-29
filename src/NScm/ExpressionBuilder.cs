namespace NScm
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class ExpressionBuilder
    {
        public static Atom Build(Tokenizer tokenizer)
        {
            return Expand(Read(tokenizer));
        }

        private static Atom Read(Tokenizer tokenizer)
        {
            string token = tokenizer.NextToken();
            return token == null
                ? AtomSymbol.EOF
                : ReadAhead(tokenizer, token);
        }

        private static Atom ReadAhead(Tokenizer tokenizer, string token)
        {
            if (token == null)
            {
                throw new SyntaxException("unexpected EOF");
            }

            if (token == "(")
            {
                var list = new List<Atom>();
                while (true)
                {
                    token = tokenizer.NextToken();
                    if (token != null && token == ")")
                    {
                        return new AtomList(list);
                    }
                    list.Add(ReadAhead(tokenizer, token));
                }
            }
            if (token == ")")
            {
                throw new SyntaxException("unexpected )");
            }
            if (token == "'")
            {
                Atom quoted = Read(tokenizer);
                return new[] { AtomSymbol.QUOTE, quoted }.ToAtomList();
            }
            return ParseAtom(token);
        }

        private static Atom ParseAtom(string token)
        {
            if (token == AtomBool.True.ToString())
            {
                return AtomBool.True;
            }
            if (token == AtomBool.False.ToString())
            {
                return AtomBool.False;
            }
            if (token[0] == '"')
            {
                return new AtomString(token.Substring(1, token.Length - 2));
            }
            if (AtomInt.TryParse(token, out AtomInt intVal))
            {
                return intVal;
            }
            if (AtomDouble.TryParse(token, out AtomDouble doubleVal))
            {
                return doubleVal;
            }
            return AtomSymbol.FromString(token);
        }

        private static Atom Expand(Atom x)
        {
            if (!(x is AtomList xs))
            {
                return x;
            }
            Syntax.Assert(xs.Count > 0, xs);
            if (AtomSymbol.QUOTE.Equals(xs[0]))
            {
                Syntax.Assert(xs.Count == 2, xs);
                return xs;
            }
            if (AtomSymbol.IF.Equals(xs[0]))
            {
                if (xs.Count == 3)
                {
                    xs = new AtomList(xs.Concat(new[] { AtomVoid.Instance }));
                }
                Syntax.Assert(xs.Count == 4, xs);
                return xs.Select(Expand).ToAtomList();
            }
            if (AtomSymbol.DEFINE.Equals(xs[0]))
            {
                Syntax.Assert(xs.Count >= 3, xs);
                var def = (AtomSymbol)xs[0];
                Atom v = xs[1];
                var body = xs.Skip(2).ToAtomList();
                if (v is AtomList args)
                {
                    Syntax.Assert(args.Count > 0, xs);
                    Atom f = args[0];
                    var parametrs = args.Skip(1).ToAtomList();
                    return Expand(new[] { def, f, Enumerable.Concat(new Atom[] { AtomSymbol.LAMBDA, parametrs }, body).ToAtomList() }.ToAtomList());
                }
                else
                {
                    Syntax.Assert(xs.Count == 3, xs);
                    Syntax.Assert(v is AtomSymbol, xs);
                    Atom expr = Expand(xs[2]);
                    return new[] { AtomSymbol.DEFINE, v, expr }.ToAtomList();
                }
            }
            if (AtomSymbol.BEGIN.Equals(xs[0]))
            {
                if (xs.Count == 1)
                    return AtomVoid.Instance;
                return xs.Select(Expand).ToAtomList();
            }
            if (AtomSymbol.LAMBDA.Equals(xs[0]))
            {
                Syntax.Assert(xs.Count >= 3, xs);
                Atom vars = xs[1];
                Syntax.Assert(vars is AtomSymbol || (vars is AtomList list && list.All(v => v is AtomSymbol)), xs);

                Atom body;
                if (xs.Count == 3)
                {
                    body = xs[2];
                }
                else
                {
                    body = Enumerable.Concat(new[] { AtomSymbol.BEGIN }, xs.Skip(2)).ToAtomList();
                }

                return new[] { AtomSymbol.LAMBDA, vars, Expand(body) }.ToAtomList();
            }
            return xs.Select(Expand).ToAtomList();
        }
    }
}
