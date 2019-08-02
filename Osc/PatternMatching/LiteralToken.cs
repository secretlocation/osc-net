using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Osc.PatternMatching
{
    public class LiteralToken : Token
    {
        public LiteralToken(string value) : base(value)
        {
        }

        public static LiteralToken Scan(string s)
        {
            var value = s.Split(Terminators, 2)[0]; 
            
            return new LiteralToken(value);
        }
        
        public override string ToRegEx()
        {
            return Regex.Escape(Value);
        }
        
        private static readonly char[] Terminators = new char[] {'?', '*', '[', '{'};
    }
}