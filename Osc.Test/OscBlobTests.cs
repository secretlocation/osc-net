using System;
using Xunit;

namespace Osc.Test
{
    public class OscBlobTests
    {
        [Fact]
        public void Ctor_NullValue_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new OscBlob(null));
        }
        
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('b', new OscBlob(new byte[0]).TypeTag);
        }

        [Fact]
        public void ToBytes_EmptyArray_ReturnsSizeCountOnly()
        {
            var sut = new OscBlob(new byte[] {});
            var expectedBytes = new byte[] {0, 0, 0, 0};
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
        
        [Fact]
        public void ToBytes_OneByteValue_PadsZeros()
        {
            var sut = new OscBlob(new byte[] { 1 });
            var expectedBytes = new byte[] { 0, 0, 0, 1, 1, 0, 0, 0 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
    }
}