using System;
using Xunit;

namespace Osc.Test
{
    public class OscStringTests
    {
        [Fact]
        public void Ctor_NullValue_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new OscString(null));
        }
        
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('s', new OscString(string.Empty).TypeTag);
        }

        [Fact]
        public void ToBytes_EmptyString_ReturnsFourNulls()
        {
            var sut = new OscString(string.Empty);
            var expectedBytes = new byte[4];
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }

        [Fact]
        public void ToBytes_ThreeLetterValue_ReturnsNullTerminatedAscii()
        {
            var sut = new OscString("OSC");
            var expectedBytes = new byte[] { 79, 83, 67, 0 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
        
        [Fact]
        public void ToBytes_UnicodeValue_SubstitutesQuestionMarks()
        {
            var sut = new OscString("ØSC");
            var expectedBytes = new byte[] { 63, 83, 67, 0 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
        
        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("abcd")]
        [InlineData("abcde")]
        public void FromBytes_ReturnsValue(string s)
        {
            var value = new OscString(s);
            var bytes = value.ToBytes();
            
            Assert.Equal(value, OscString.FromBytes(ref bytes));
            Assert.Empty(bytes);
        }
    }
}