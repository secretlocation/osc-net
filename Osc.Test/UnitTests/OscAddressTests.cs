using System;
using Xunit;

namespace Osc.Test
{
    public class OscAddressTests
    {
        [Fact]
        public void Ctor_NullString_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new OscAddress((string)null));
        }
        
        [Fact]
        void Ctor_EmptyString_Throws()
        {
            Assert.Throws<ArgumentException>(() => new OscAddress(""));
        }
        
        [Fact]
        void Ctor_StringWithoutLeadingSlash_Throws()
        {
            Assert.Throws<ArgumentException>(() => new OscAddress("abs"));
        }

        
        [Fact]
        void Ctor_StringWithLeadingSlash_OneSegment()
        {
            var sut = new OscAddress("/abc");
            
            Assert.Collection(sut.Segments, segment => Assert.Equal("abc", segment));
        }
        
        [Fact]
        void Ctor_StringWithTrailingSlash_EmptySegment()
        {
            var sut = new OscAddress("/abc/");
            
            Assert.Collection(sut.Segments, 
                segment => Assert.Equal("abc", segment),
                segment => Assert.Equal("", segment));
        }
        
        
        [Fact]
        void Ctor_StringWithConsecutiveSlashes_EmptySegments()
        {
            var sut = new OscAddress("/a//bc");
            
            Assert.Collection(sut.Segments, 
                segment => Assert.Equal("a", segment),
                segment => Assert.Equal("", segment),
                segment => Assert.Equal("bc", segment));
        }

        [Fact]
        void Ctor_StringWithMultipleSlashes_MultipleSegments()
        {
            var sut = new OscAddress("/a/b/c");
            
            Assert.Collection(sut.Segments, 
                segment => Assert.Equal("a", segment),
                segment => Assert.Equal("b", segment),
                segment => Assert.Equal("c", segment));
        }

        [Fact]
        public void IsValid_BasicAddress_True()
        {
            var sut = new OscAddress("/abc");

            var result = sut.IsValid(out var messages);
            
            Assert.True(result);
            Assert.Empty(messages);
        }
        
        [Fact]
        public void IsValid_EmptySegment_False()
        {
            var sut = new OscAddress("/a//bc");

            var result = sut.IsValid(out var messages);
            
            Assert.False(result);
            Assert.Collection(messages,
                m => Assert.Equal("1: Segment cannot be empty.", m));
        }
        
        [Fact]
        public void IsValid_IllegalCharsSegment_False()
        {
            var sut = new OscAddress("/a /b#*,?[]{}");

            var result = sut.IsValid(out var messages);
            
            Assert.False(result);
            Assert.Collection(messages,
                m => Assert.Equal("0: Segment may not contain ' '.", m),
                m => Assert.Equal("1: Segment may not contain '#'.", m),
                m => Assert.Equal("1: Segment may not contain '*'.", m),
                m => Assert.Equal("1: Segment may not contain ','.", m),
                m => Assert.Equal("1: Segment may not contain '?'.", m),
                m => Assert.Equal("1: Segment may not contain '['.", m),
                m => Assert.Equal("1: Segment may not contain ']'.", m),
                m => Assert.Equal("1: Segment may not contain '{'.", m),
                m => Assert.Equal("1: Segment may not contain '}'.", m));
        }
    }
}