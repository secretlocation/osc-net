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

        public void Send(OscMessage oscMessage)
        {
            if (udpClient == null)
                udpClient = new UdpClient();
            
            var bytes = oscMessage.ToBytes();
            
            udpClient.Send(bytes, bytes.Length, remoteEndPoint);
        }

        public void Dispose()
        {
            udpClient?.Dispose();
        }
    }
}
