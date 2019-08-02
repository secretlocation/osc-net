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
            
            Assert.Equal("^.*$", regex.ToString());
        }

        [Theory]
        [InlineData("/abc", "/*")]
        [InlineData("/abc", "/?*")]
        [InlineData("/abc", "/*?")]
        [InlineData("/abc", "/???")]
        public void Match_MatchingAddressAndPattern_True(string addressString, string patternString)
        {
            var sut = new Interpreter(new Lexer());
            var address = new Address(addressString);
            var pattern = new AddressPattern(patternString);
            
            Assert.True(sut.Match(address, pattern));
        }
    }
}