namespace NScm.CLI
{
    using System;
    using System.IO;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var interpreter = new Interpreter();

            if (args.Length > 0)
            {
                string fileName = args[0];

                if (!File.Exists(args[0]))
                {
                    throw new FileNotFoundException(string.Format("File does not exist {0}", fileName));
                }

                using TextReader reader = new StreamReader(fileName);
                interpreter.REPL(reader, Console.Out);
            }
            else
            {
                interpreter.REPL(Console.In, Console.Out, "> ");
            }
        }
    }
}
