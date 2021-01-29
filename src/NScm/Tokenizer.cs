namespace NScm
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    internal class Tokenizer
    {
        private static readonly Regex tokenizer = new Regex(
            @"^\s*([(')]|""(?:[\\].|[^\\""])*""|;.*|[^\s('"",;)]*)(.*)",
            RegexOptions.Compiled
        );

        private readonly TextReader file;
        private string line;

        public Tokenizer(TextReader file)
        {
            this.file = file ?? throw new ArgumentNullException(nameof(file));
            this.line = string.Empty;
        }

        public string NextToken()
        {
            while (true)
            {
                if (line == string.Empty)
                {
                    line = file.ReadLine();
                }
                if (line == string.Empty)
                {
                    continue;
                }
                else if (line == null)
                {
                    return null;
                }
                else
                {
                    Match res = tokenizer.Match(line);
                    string token = res.Groups[1].Value;
                    line = res.Groups[2].Value;

                    if (string.IsNullOrEmpty(token))
                    {
                        string current = line;
                        line = string.Empty;
                        if (current.Trim() != string.Empty)
                        {
                            throw new SyntaxException(string.Format("{0}: {1}", "unexpected syntax", current));
                        }
                    }

                    if (!string.IsNullOrEmpty(token) && !token.StartsWith(";"))
                    {
                        return token;
                    }
                }
            }
        }
    }
}
