using System;
using Xunit;

namespace OscPack.Test
{
    public class OscBlobArgumentTests
    {
        [Fact]
        public void Ctor_NullValue_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new OscBlobArgument(null));
        }
        
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('b', new OscBlobArgument(new byte[0]).TypeTag);
        }

        [Fact]
        public void ToBytes_EmptyArray_ReturnsSizeCountOnly()
        {
            var sut = new OscBlobArgument(new byte[] {});
            var expectedBytes = new byte[] {0, 0, 0, 0};
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
        
        [Fact]
        public void ToBytes_OneByteValue_PadsZeros()
        {
            var sut = new OscBlobArgument(new byte[] { 1 });
            var expectedBytes = new byte[] { 0, 0, 0, 1, 1, 0, 0, 0 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
    }
}