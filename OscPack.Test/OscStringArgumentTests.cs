using System;
using Xunit;

namespace OscPack.Test
{
    public class OscStringArgumentTests
    {
        [Fact]
        public void Ctor_NullValue_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new OscStringArgument(null));
        }
        
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('s', new OscStringArgument(string.Empty).TypeTag);
        }

        [Fact]
        public void ToBytes_EmptyString_ReturnsFourNulls()
        {
            var sut = new OscStringArgument(string.Empty);
            var expectedBytes = new byte[0];
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }

        [Fact]
        public void ToBytes_ThreeLetterValue_ReturnsNullTerminatedAscii()
        {
            var sut = new OscStringArgument("OSC");
            var expectedBytes = new byte[] { 79, 83, 67, 0 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
        
        [Fact]
        public void ToBytes_UnicodeValue_SubstitutesQuestionMarks()
        {
            var sut = new OscStringArgument("ØSC");
            var expectedBytes = new byte[] { 63, 83, 67, 0 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
    }
}