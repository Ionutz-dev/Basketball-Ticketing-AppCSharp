using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace networking
{
    public abstract class AbstractServer
    {
        private TcpListener server;
        private readonly string host;
        private readonly int port;

        public AbstractServer(string host, int port)
        {
            this.host = host;
            this.port = port;
        }

        public void Start()
        {
            var address = IPAddress.Parse(host);
            var endpoint = new IPEndPoint(address, port);
            server = new TcpListener(endpoint);
            server.Start();
            Console.WriteLine($"Server started on {host}:{port}");

            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for clients ...");
                    var client = server.AcceptTcpClient();
                    Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

                    ProcessRequest(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AbstractServer] Error accepting client: {ex.Message}");
                }
            }
        }

        protected abstract void ProcessRequest(TcpClient client);
    }

    public abstract class ConcurrentServer : AbstractServer
    {
        protected ConcurrentServer(string host, int port) : base(host, port)
        {
        }

        protected override void ProcessRequest(TcpClient client)
        {
            var worker = CreateWorker(client);
            worker.Start();
        }

        protected abstract Thread CreateWorker(TcpClient client);
    }
}