using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Osc
{
    public class OscServer : IDisposable
    {
        private UdpClient udpClient;
        private bool started;
        private bool disposing;
        private readonly HashSet<OscMethod> methodSet = new HashSet<OscMethod>();
        private readonly object methodsLock = new object();
        private readonly int localPort;
        private readonly IPEndPoint remoteEndPoint;


        public OscServer(int localPort, IPEndPoint remoteEndPoint)
        {
            this.localPort = localPort;
            this.remoteEndPoint = remoteEndPoint;
        }

        public void AddMethods(params OscMethod[] oscMethods)
        {
            lock (methodsLock)
            {
                foreach (var method in oscMethods)
                {
                    methodSet.Add(method);
                }
            }
        }

        public void RemoveMethods(params OscMethod[] oscMethods)
        {
            lock (methodsLock)
            {
                foreach (var method in oscMethods)
                {
                    methodSet.Remove(method);
                }
            }
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
                var message = OscMessage.FromBytes(bytes);

                lock (methodsLock)
                {
                    // Invoke each method that has the same OSC Address pattern
                    foreach (var method in methodSet.Where(method => method.OscAddress.Segments.SequenceEqual(message.AddressPattern.Segments)))
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
