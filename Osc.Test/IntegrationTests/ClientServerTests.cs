using System.Net;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace Osc.Test
{
    public class ClientServerTests
    {
        private readonly ITestOutputHelper output;
        private bool messageReceived;

        public ClientServerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void SendAndReceiveMessage()
        {
            using (var server = new OscServer(9001, new IPEndPoint(IPAddress.Any, 0)))
            using (var client = new OscClient(new IPEndPoint(IPAddress.Loopback, 9001)))
            {
                var method = new OscMethod(new OscAddress("/abc"), Callback);
            
                server.AddMethods(method);
                server.Start();
            
                var addressPattern = new OscAddressPattern("/abc");
                var arguments = new OscValue[] {new OscString("Hello World.")};
                var message = new OscMessage(addressPattern, arguments);
            
                client.Send(message);
            
                Thread.Sleep(100);
            }
            
            Assert.True(messageReceived);
        }

        private void Callback(OscMessage oscMessage)
        {
            messageReceived = true;
            output.WriteLine(oscMessage.ToString());
        }
    }
}