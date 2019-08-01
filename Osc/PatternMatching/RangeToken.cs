namespace Osc.PatternMatching
{
    public class RangeToken : Token
    {
        public RangeType Type { get; }
        public string LowerBound { get; }
        public string UpperBound { get; }

        public RangeToken(string value, RangeType type, string lowerBound, string upperBound) : base(value)
        {
            Type = type;
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }
    }
}