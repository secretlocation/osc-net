using Xunit;

namespace OscPack.Test
{
    public class OscFloat32ArgumentTests
    {
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('f', new OscFloat32Argument(0).TypeTag);
        }

        [Fact]
        public void ToBytes_ReturnsBigEndianIEEE754()
        {
            var sut = new OscFloat32Argument(-4.360311e+25f);
            var expectedBytes = new byte[] { 0xEA, 0x10, 0x45, 0x45 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
    }
}