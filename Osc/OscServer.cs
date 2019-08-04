using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Osc
{
    public class OscServer : IDisposable
    {
        private UdpClient udpClient;
        private bool started;
        private bool disposing;
        private readonly List<Method> methods = new List<Method>();
        private readonly object methodsLock = new object();
        private readonly int localPort;
        private readonly IPEndPoint remoteEndPoint;


        public OscServer(int localPort, IPEndPoint remoteEndPoint)
        {
            this.localPort = localPort;
            this.remoteEndPoint = remoteEndPoint;
        }

        public void Start()
        {
            if (!started)
            {
                started = true;
                udpClient = new UdpClient(localPort);
                BeginReceive();
            }
        }

        public void AddMethods(params Method[] methods)
        {
            lock (methodsLock)
            {
                foreach (var method in methods)
                {
                    this.methods.Add(method);
                }
            }
        }

        private void BeginReceive()
        {
            var callback = new AsyncCallback(EndReceive);
          
            udpClient.BeginReceive(callback, new object());
        }

        private void EndReceive(IAsyncResult result)
        {
            try
            {
                var endPoint = new IPEndPoint(remoteEndPoint.Address, remoteEndPoint.Port);
                var bytes = udpClient.EndReceive(result, ref endPoint);
                var message = Message.FromBytes(bytes);

                lock (methodsLock)
                {
                    foreach (var method in methods)
                    {
                        method.Dispatch(message);
                    }
                }

                BeginReceive();
            }
            catch (ObjectDisposedException)
            {
                if (!disposing)
                    throw;
            }
        }
        
        public void Dispose()
        {
            disposing = true;
            udpClient?.Dispose();
        }
    }
}
