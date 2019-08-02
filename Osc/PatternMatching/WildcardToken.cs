using System;

namespace Osc.PatternMatching
{
    public class WildcardToken : Token
    {
        public WildcardType Type { get; }

        public WildcardToken(char value, WildcardType type) : base(value.ToString())
        {
            Type = type;
        }
        
        public override string ToRegEx()
        {
            return Type == WildcardType.Single
                ? "."
                : ".*";
        }
        
        public static WildcardToken Scan(string s)
        {
            switch (s[0])
            {
                case '?' : return new WildcardToken(s[0], WildcardType.Single);
                case '*' : return new WildcardToken(s[0], WildcardType.Multiple);
                
                default: return null;
            }
        }
    }
}