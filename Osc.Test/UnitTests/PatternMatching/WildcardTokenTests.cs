using Osc.PatternMatching;
using Xunit;

namespace Osc.Test.PatternMatching
{
    public class WildcardTokenTests
    {
        [Fact]
        public void Scan_StringStartsWithNonWildcardChar_Null()
        {
            Assert.Null(WildcardToken.Scan("abc"));
        }
        
        [Fact]
        public void Scan_StringStartsQuestionMark_SingleWildCardToken()
        {
            var token = WildcardToken.Scan("?bc");
            
            Assert.NotNull(token);
            Assert.Equal(WildcardType.Single, token.Type);
            Assert.Equal("?", token.Value);
        }
        
        [Fact]
        public void Scan_StringStartsAsterisk_MultipleWildCardToken()
        {
            var token = WildcardToken.Scan("*bc");
            
            Assert.NotNull(token);
            Assert.Equal(WildcardType.Multiple, token.Type);
            Assert.Equal("*", token.Value);
        }
    }
}