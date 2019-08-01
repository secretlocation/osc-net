using System;
using System.Collections.Generic;

namespace Osc.PatternMatching
{
    public class Lexer
    {
        private string current = String.Empty;
        
        public List<Token> GetTokens(string segment)
        {
            if (segment == null)
                throw new ArgumentNullException(nameof(segment));
            
            var tokens = new List<Token>();

            while (segment.Length > 0)
            {
            }

            return tokens;
        }

        private Token GetNextToken(string s)
        {
            return ScanWildcard(s)
                ?? ScanRange(s);
        }

        private Token ScanRange(string s)
        {
            if (!s.StartsWith("["))
                return null;

            var closingBracketIndex = s.IndexOf(']');

            if (closingBracketIndex < 0)
                throw new LexerException($"Missing closing bracket in range: {s}");

            var tokenValue = s.Substring(0, closingBracketIndex + 1);

            var separatorIndex = tokenValue.IndexOf("-", 0, StringComparison.InvariantCulture);
            
            if (separatorIndex < 0)
                throw new LexerException($"Missing separator in range: {s}");

            var lowerBound = tokenValue.Substring(1, separatorIndex);
            var rangeType = RangeType.InRange;

            if (lowerBound.StartsWith("!"))
            {
                rangeType = RangeType.NotInRange;
                lowerBound = lowerBound.Substring(1, lowerBound.Length - 1);
            }
            
            if (lowerBound.Length == 0)
                throw new LexerException($"Missing lower bound in range: {s}");
            
            if (lowerBound.Length > 1)
                throw new LexerException($"Invalid lower bound in range: {s}");
            
            var upperBound = tokenValue.Substring(separatorIndex + 1, tokenValue.Length - separatorIndex - 1);

            if (upperBound.Length == 0)
                throw new LexerException($"Missing upper bound in range: {s}");

            if (upperBound.Length > 1)
                throw new LexerException($"Invalid upper bound in range: {s}");

            return new RangeToken(tokenValue, rangeType, lowerBound, upperBound);
        }

        public class LexerException : Exception
        {
            public LexerException(string message) : base(message)
            {
            }            
        }

        private Token ScanWildcard(string s)
        {
            var value = s[0];

            switch (value)
            {
                case '?' : return new WildcardToken(value, WildcardType.Single);
                case '*' : return new WildcardToken(value, WildcardType.Multiple);
                default: return null;
            }
        }
    }
}