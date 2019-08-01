namespace Osc.PatternMatching
{
    public class WildcardToken : Token
    {
        public WildcardType Single { get; }

        public WildcardToken(char value, WildcardType single) : base(value.ToString())
        {
            Single = single;
        }
    }
}