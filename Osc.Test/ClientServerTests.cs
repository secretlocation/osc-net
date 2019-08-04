using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                var method = new Method(new Address("/abc"), Callback);
            
                server.AddMethods(method);
                server.Start();
            
                var addressPattern = new AddressPattern("/abc");
                var arguments = new OscValue[] {new OscString("Hello World.")};
                var message = new Message(addressPattern, arguments);
            
                client.Send(message);
            
                Thread.Sleep(100);
            }
            
            Assert.True(messageReceived);
        }

        private void Callback(Message message)
        {
            messageReceived = true;
            output.WriteLine(message.ToString());
        }
    }
}