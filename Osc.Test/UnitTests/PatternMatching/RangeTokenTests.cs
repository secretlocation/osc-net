using Osc.PatternMatching;
using Xunit;

namespace Osc.Test.PatternMatching
{
    public class RangeTokenTests
    {
        [Fact]
        public void Scan_MissingOpeningBrace_Null()
        {
            Assert.Null(RangeToken.Scan("ab-c]"));
        }
        
        [Theory]
        [InlineData("[")]
        [InlineData("[a")]
        [InlineData("[a-")]
        [InlineData("[a-c")]
        [InlineData("[!")]
        [InlineData("[!a")]
        [InlineData("[!a-")]
        [InlineData("[!a-c")]
        [InlineData("[ac]")]
        [InlineData("[abc]")]
        public void Scan_IncompleteRange_Exception(string s)
        {
            Assert.Throws<OscLexerException>(() => RangeToken.Scan(s));
        }
        
        [Fact]
        public void Scan_Range_RangeToken()
        {
            var token = RangeToken.Scan("[a-c]");
            
            Assert.NotNull(token);
            Assert.Equal(RangeType.InRange, token.Type);
            Assert.Equal('a', token.LowerBound);
            Assert.Equal('c', token.UpperBound);
            Assert.Equal("[a-c]", token.Value);
        }
        
        [Fact]
        public void Scan_RangeFollowedByLiteral_LiteralIsIgnored()
        {
            var token = RangeToken.Scan("[a-c]de");
            
            Assert.NotNull(token);
            Assert.Equal("[a-c]", token.Value);
        }

        [Fact]
        public void Scan_RangeWithExclamationMark_RangeToken()
        {
            var token = RangeToken.Scan("[!a-c]");
            
            Assert.NotNull(token);
            Assert.Equal(RangeType.NotInRange, token.Type);
            Assert.Equal('a', token.LowerBound);
            Assert.Equal('c', token.UpperBound);
            Assert.Equal("[!a-c]", token.Value);
        }
        
        [Fact]
        public void Scan_NotInRangeFollowedByLiteral_LiteralIsIgnored()
        {
            var token = RangeToken.Scan("[!a-c]de");
            
            Assert.NotNull(token);
            Assert.Equal("[!a-c]", token.Value);
        }
    }
}