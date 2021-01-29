namespace NScm
{
    public class AtomProcedure : Atom, IAtomProcedure
    {
        private readonly Atom Parameters;
        private readonly Atom Body;
        private readonly Environment Environment;

        public AtomProcedure(Atom parameters, Atom body, Environment env)
        {
            this.Parameters = parameters;
            this.Body = body;
            this.Environment = env;
        }

        public Atom Call(AtomList args) =>
            ExpressionParser.Evaluate(Body, Environment.CreateChild(Parameters, args));

        public override string ToString() => "#<Procedure>";
    }
}
