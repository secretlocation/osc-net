using System;
using System.Text.RegularExpressions;

namespace Osc.PatternMatching
{
    public class RangeToken : Token
    {
        public RangeType Type { get; }
        public char LowerBound { get; }
        public char UpperBound { get; }

        public RangeToken(string value, RangeType type, char lowerBound, char upperBound) : base(value)
        {
            Type = type;
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }
        
        public override string ToRegEx()
        {
            return Type == RangeType.InRange
                ? $"[{Regex.Escape(LowerBound.ToString())}-{Regex.Escape(UpperBound.ToString())}]"
                : $"[^{Regex.Escape(LowerBound.ToString())}-{Regex.Escape(UpperBound.ToString())}]";
        }

        public static RangeToken Scan(string s)
        {
            var slicer = new StringSlicer(s);
 
            if (slicer.NextChar() != '[')
                return null;
            
            if (s.Length < 5 || (s.Contains("!") && s.Length < 6)) 
                throw new LexerException($"Incomplete range expression: {s}");

            var type = RangeType.InRange;
            var lowerBound = slicer.NextChar();

            if (lowerBound == '!')
            {
                type = RangeType.NotInRange;
                lowerBound = slicer.NextChar();
            }

            if (slicer.NextChar() != '-')
                throw new LexerException($"Missing separator in range expression: {s}");
            
            var upperBound = slicer.NextChar();

            if (slicer.NextChar() != ']')
                throw new LexerException($"Missing closing bracket in range expression: {s}");

            var value = s.Substring(0, s.Length - slicer.Length);
            
            return new RangeToken(value, type, lowerBound, upperBound);
        }

        private class StringSlicer
        {
            private string value;

            public int Length => value.Length;
            public bool IsEmpty => value == string.Empty;
            
            public StringSlicer(string s)
            {
                value = s;
            }

            public char NextChar()
            {
                var c = value[0];
                value = value.Substring(1);

                return c;
            }
        }
    }
 }