using Osc.PatternMatching;
using Xunit;

namespace Osc.Test.PatternMatching
{
    public class InterpreterTests
    {
        [Fact]
        public void GetRegex_Tokens_Regex()
        {
            var tokens = new Token[]
            {
                new WildcardToken('?', WildcardType.Single),                 
                new WildcardToken('*', WildcardType.Multiple),                 
            };
            
            var sut = new Interpreter(new Lexer());
            var regex = sut.GetRegex(tokens);
            
            Assert.Equal("^..*$", regex.ToString());
        }

        [Theory]
        [InlineData("/abc", "/*")]
        [InlineData("/abc", "/?*")]
        [InlineData("/abc", "/*?")]
        [InlineData("/abc", "/???")]
        [InlineData("/abc", "/a[a-c]c")]
        [InlineData("/abc", "/[!b-c]bc")]
        [InlineData("/abc", "/{abc,def}")]
        [InlineData("/abc", "/abc")]
        [InlineData("/abc/def", "/abc/def")]
        [InlineData("/aabdefaddefcd/def", "/a?b*[a-c][!a-c]{adc,def}cd/def")]
        public void Match_MatchingAddressAndPattern_True(string addressString, string patternString)
        {
            var sut = new Interpreter(new Lexer());
            var address = new OscAddress(addressString);
            var pattern = new OscAddressPattern(patternString);
            
            Assert.True(sut.Match(address, pattern));
        }
        
        [Theory]
        [InlineData("/abc", "/??")]
        [InlineData("/abc", "/.*")]
        [InlineData("/abc/def", "/*/d.")]
        public void Match_NonMatchingAddressAndPattern_False(string addressString, string patternString)
        {
            var sut = new Interpreter(new Lexer());
            var address = new OscAddress(addressString);
            var pattern = new OscAddressPattern(patternString);
            
            Assert.False(sut.Match(address, pattern));
        }
    }
}