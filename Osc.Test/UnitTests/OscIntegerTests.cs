using Xunit;

namespace Osc.Test
{
    public class OscIntegerTests
    {
        [Fact]
        public void TypeTag_Get_ReturnsChar()
        {
            Assert.Equal('i', new OscInt(0).TypeTag);
        }

        [Fact]
        public void ToBytes_ReturnsBigEndian2sComplement()
        {
            var sut = new OscInt(-368032443);
            var expectedBytes = new byte[] { 0xEA, 0x10, 0x45, 0x45 };
            
            Assert.Equal(expectedBytes, sut.ToBytes());
        }
        
        [Fact]
        public void FromBytes_ReturnsValue()
        {
            var value = new OscInt(-368032443);
            var bytes = value.ToBytes();
            
            Assert.Equal(value, OscInt.FromBytes(ref bytes));
            Assert.Empty(bytes);
        }
    }
}