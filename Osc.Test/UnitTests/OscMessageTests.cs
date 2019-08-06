using Xunit;

namespace Osc.Test
{
    public class OscMessageTests
    {
        [Fact]
        public void FromBytes_MessageBytes_MessageObject()
        {
            var addressPattern = new OscAddressPattern("/abc");
            var intArgument = new OscInt(32);
            var floatArgument = new OscFloat(3.21f);
            var stringArgument = new OscString("abc");
            var blobArgument = new OscBlob(new byte[] {1, 2, 3});
            
            var message = new OscMessage(addressPattern, new OscValue[] {intArgument, floatArgument, stringArgument, blobArgument});
            var bytes = message.ToBytes();
            var messageFromBytes = OscMessage.FromBytes(bytes);
            
            Assert.Equal(message.AddressPattern, messageFromBytes.AddressPattern);
            Assert.Equal(message.Arguments.Length, messageFromBytes.Arguments.Length);
            Assert.Equal(message.Arguments, messageFromBytes.Arguments);
        }
    }
}