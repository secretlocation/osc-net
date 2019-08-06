using System.Collections;
using System.Collections.Generic;

namespace Osc.PatternMatching
{
    public class Lexer
    {
        
        public IEnumerable<Token> GetTokens(string pattern)
        {
            if (pattern == null)
                throw new OscLexerException("Cannot process null pattern.");
            
            var tokens = new List<Token>();
            
            while (pattern.Length > 0)
            {
                var token = WildcardToken.Scan(pattern)
                    ?? RangeToken.Scan(pattern)
                    ?? ListToken.Scan(pattern)
                    ?? LiteralToken.Scan(pattern) as Token;
                
                tokens.Add(token);
                pattern = pattern.Substring(token.Value.Length);
            }

            return tokens;
        }
    }
}