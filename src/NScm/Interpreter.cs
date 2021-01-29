namespace NScm
{
    using NScm.NativeProcedures;
    using System;
    using System.IO;

    public partial class Interpreter
    {
        private readonly Environment environment;

        public Interpreter()
        {
            this.environment = new Environment(NativeProcedureBuilder.Build(), Environment.CreateEmpty());
        }

        public Atom Evaluate(TextReader input)
        {
            input = input ?? throw new ArgumentNullException(nameof(input));

            var tokenizer = new Tokenizer(input);
            Atom result = null;
            while (true)
            {
                Atom expr = ExpressionBuilder.Build(tokenizer);
                if (AtomSymbol.EOF.Equals(expr))
                {
                    return result;
                }
                result = ExpressionParser.Evaluate(expr, environment);
            }
        }

        public void REPL(TextReader input, TextWriter output, string prompt = null)
        {
            input = input ?? throw new ArgumentNullException(nameof(input));
            output = output ?? throw new ArgumentNullException(nameof(output));

            var tokenizer = new Tokenizer(input);
            Atom res;
            while (true)
            {
                try
                {
                    if (prompt != null)
                    {
                        output.Write(prompt);
                    }
                    Atom expr = ExpressionBuilder.Build(tokenizer);
                    if (AtomSymbol.EOF.Equals(expr))
                    {
                        return;
                    }
                    else
                    {
                        res = ExpressionParser.Evaluate(expr, environment);
                        if (!(res is AtomVoid))
                        {
                            output.WriteLine(res.ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    ConsoleColor prev = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    output.WriteLine(e.Message);
                    Console.ForegroundColor = prev;
                }
            }
        }
    }
}
