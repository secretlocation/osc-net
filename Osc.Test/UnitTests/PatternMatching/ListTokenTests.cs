using Osc.PatternMatching;
using Xunit;

namespace Osc.Test.PatternMatching
{
    public class ListTokenTests
    {
        [Fact]
        public void Scan_MissingOpeningBrace_Null()
        {
            Assert.Null(ListToken.Scan("ab,cd,ef}"));
        }
        
        [Fact]
        public void Scan_MissingClosingBrace_Exception()
        {
            Assert.Throws<OscLexerException>(() => ListToken.Scan("{ab,cd,ef"));
        }

        [Fact]
        public void Scan_EmptyList_Token()
        {
            var token = ListToken.Scan("{}");
            
            Assert.NotNull(token);
            Assert.Equal("{}", token.Value);
            Assert.Empty(token.List);
        }
        
        [Fact]
        public void Scan_EmptyListEntry_EntryIsIgnored()
        {
            var token = ListToken.Scan("{a,}");
            
            Assert.NotNull(token);
            Assert.Equal("{a,}", token.Value);
            Assert.Collection(token.List, 
                entry => Assert.Equal("a", entry));
        }
        
        [Fact]
        public void Scan_ValidList_Token()
        {
            var token = ListToken.Scan("{a,bc,def}");
            
            Assert.NotNull(token);
            Assert.Equal("{a,bc,def}", token.Value);
            Assert.Collection(token.List, 
                entry => Assert.Equal("a", entry),
                entry => Assert.Equal("bc", entry),
                entry => Assert.Equal("def", entry));
        }
    }
}