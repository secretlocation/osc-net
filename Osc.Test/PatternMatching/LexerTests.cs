using System;
using Osc.PatternMatching;
using Xunit;

namespace Osc.Test.PatternMatching
{
    public class LexerTests
    {
        [Fact]
        public void GetTokens_Null_Throws()
        {
            var sut = new Lexer();
            
            Assert.Throws<LexerException>(() => sut.GetTokens(null));
        }

        [Fact]
        public void GetTokens_EmptyString_EmptyTokenCollection()
        {
            var sut = new Lexer();
            var tokens = sut.GetTokens(String.Empty);
            
            Assert.NotNull(tokens);
            Assert.Empty(tokens);
        }


        [Fact]
        public void GetTokens_ValidPattern_Tokens()
        {
            var sut = new Lexer();
            var tokens = sut.GetTokens("?*[a-b][!a-b]{a,bc}abc");

            Assert.Collection(tokens,
                token => ValidateSingleWildcard(token as WildcardToken),
                token => ValidateMultipleWildcard(token as WildcardToken),
                token => ValidateInRange(token as RangeToken),
                token => ValidateNotInRange(token as RangeToken),
                token => ValidateList(token as ListToken),
                token => ValidateLiteral(token as LiteralToken));
            
            void ValidateSingleWildcard(WildcardToken token)
            {
                
            }
            
            void ValidateMultipleWildcard(WildcardToken token)
            {
                
            }

            void ValidateInRange(RangeToken token)
            {
                
            }

            void ValidateNotInRange(RangeToken token)
            {
                
            }

            void ValidateList(ListToken token)
            {
                
            }

            void ValidateLiteral(LiteralToken token)
            {
                
            }



        }
    }
}