using System;
using System.Net;
using System.Net.Sockets;

namespace Osc
{
    public class OscClient : IDisposable
    {
        private readonly IPEndPoint remoteEndPoint;
        private UdpClient udpClient;

        public OscClient(IPEndPoint remoteEndPoint)
        {
            this.remoteEndPoint = remoteEndPoint;
        }

        public void Send(Message message)
        {
            if (udpClient == null)
                udpClient = new UdpClient();
            
            var bytes = message.ToBytes();
            
            udpClient.Send(bytes, bytes.Length, remoteEndPoint);
        }

        public void Dispose()
        {
            udpClient?.Dispose();
        }
    }
}
