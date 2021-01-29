namespace NScm
{
    using System;

    public class SyntaxException : Exception
    {
        public SyntaxException(string msg) : base(msg)
        {
        }
    }
}
