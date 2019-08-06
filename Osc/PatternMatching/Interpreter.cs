using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Osc.PatternMatching
{
    public class Interpreter
    {
        private Lexer lexer;

        public Interpreter(Lexer lexer)
        {
            this.lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
        }
        
        public Regex GetRegex(IEnumerable<Token> tokens)
        {
            var regExBuilder = new StringBuilder();

            regExBuilder.Append("^");

            foreach (var token in tokens)
            {
                regExBuilder.Append(token.ToRegEx());
            }

            regExBuilder.Append("$");
            
            return new Regex(regExBuilder.ToString());
        }

        public bool Match(OscAddress oscAddress, OscAddressPattern pattern)
        {
            if (oscAddress.Segments.Length != pattern.Segments.Length)
                return false;

            for (var i = 0; i < oscAddress.Segments.Length; i++)
            {
                var tokens = lexer.GetTokens(pattern.Segments[i]);
                var regex = GetRegex(tokens);

                if (!regex.IsMatch(oscAddress.Segments[i]))
                    return false;
            }

            return true;
        }
    }
}