using Xunit;

namespace Osc.Test
{
    public class Integer32ArgumentTests
    {
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('i', new Integer32Argument(0).TypeTag);
        }

        [Fact]
        public void ToBytes_ReturnsBigEndian2sComplement()
        {
            var sut = new Integer32Argument(-368032443);
            var expectedBytes = new byte[] { 0xEA, 0x10, 0x45, 0x45 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
    }
}