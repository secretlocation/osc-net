namespace Osc.PatternMatching
{
    public abstract class Token
    {
        public string Value { get; }

        protected Token(string value)
        {
            Value = value;
        }
    }
}