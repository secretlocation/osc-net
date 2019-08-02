using Osc.PatternMatching;
using Xunit;

namespace Osc.Test.PatternMatching
{
    public class LiteralTokenTests
    {
        [Fact]
        public void Scan_SimpleLiteral_Token()
        {
            var token = LiteralToken.Scan("abc");

            Assert.NotNull(token);
            Assert.Equal("abc", token.Value);
        }
        
        [Theory]
        [InlineData("abc")]
        [InlineData("abc?")]
        [InlineData("abc*")]
        [InlineData("abc[")]
        [InlineData("abc{")]
        public void Scan_Pattern_Token(string s)
        {
            var token = LiteralToken.Scan(s);

            Assert.NotNull(token);
            Assert.Equal("abc", token.Value);
        }
    }
}