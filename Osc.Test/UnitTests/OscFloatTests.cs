using Xunit;

namespace Osc.Test
{
    public class OscFloatTests
    {
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('f', new OscFloat(0).TypeTag);
        }

        [Fact]
        public void ToBytes_ReturnsBigEndianIEEE754()
        {
            var sut = new OscFloat(-4.360311e+25f);
            var expectedBytes = new byte[] { 0xEA, 0x10, 0x45, 0x45 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
        
         
        [Fact]
        public void FromBytes_ReturnsValue()
        {
            var value = new OscFloat(-4.360311e+25f);
            var bytes = value.ToBytes();
            
            Assert.Equal(value, OscFloat.FromBytes(ref bytes));
            Assert.Empty(bytes);
        }
    }
}