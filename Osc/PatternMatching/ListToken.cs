using System;
using System.Linq;

namespace Osc.PatternMatching
{
    public class ListToken : Token
    {
        public string[] List { get; }
        
        public ListToken(string value, string[] list) : base(value)
        {
            List = list;
        }
        
        public override string ToRegEx()
        {
            return $"({string.Join("|", List)})";
        }

        public static ListToken Scan(string s)
        {
            if (!s.StartsWith("{"))
                return null;

            var closingBraceIndex = s.IndexOf("}", StringComparison.InvariantCulture);

            if (closingBraceIndex < 0)
                throw new LexerException($"Missing closing brace in list expression: {s}");

            var value = s.Substring(0, closingBraceIndex + 1);
            var list = value.Substring(1, value.Length - 2).Split(ListSeparator, StringSplitOptions.RemoveEmptyEntries);
            
            return new ListToken(value, list);
        }
    
        private static readonly char[] ListSeparator = new char[] {','};
    }
}