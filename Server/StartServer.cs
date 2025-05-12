using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using networking;
using persistence;
using persistence;
using services;
using Server;

namespace Server
{
    internal static class StartServer
    {
        private const int DefaultPort = 55555;
        private const string DefaultIp = "127.0.0.1";

        private static void Main(string[] args)
        {
            Console.WriteLine("Reading properties from app.config ...");

            int port = DefaultPort;
            string ip = DefaultIp;

            var portS = ConfigurationManager.AppSettings["port"];
            if (portS != null && int.TryParse(portS, out var parsedPort))
            {
                port = parsedPort;
            }
            else
            {
                Console.WriteLine("Port property not found or invalid. Using default " + DefaultPort);
            }

            var ipS = ConfigurationManager.AppSettings["ip"];
            if (ipS != null)
            {
                ip = ipS;
            }

            Console.WriteLine("Configuration Settings for database {0}", GetConnectionStringByName("identifierDB"));

            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("identifierDB"));

            IUserRepository userRepo = new UserRepositoryDb(props);
            IMatchRepository matchRepo = new MatchRepositoryDb(props);
            ITicketRepository ticketRepo = new TicketRepositoryDb(props);

            IBasketballServices serviceImpl = new BasketballServerImpl(userRepo, matchRepo, ticketRepo);

            Console.WriteLine("Starting Basketball Server on IP {0} and port {1}", ip, port);
            var server = new BasketballRpcConcurrentServer(ip, port, serviceImpl);
            server.Start();
            Console.WriteLine("Server started ...");
            Console.ReadLine();
        }

        private static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }
    }

    internal class BasketballRpcConcurrentServer : ConcurrentServer
    {
        private readonly IBasketballServices server;

        public BasketballRpcConcurrentServer(string host, int port, IBasketballServices server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("BasketballRpcConcurrentServer...");
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            var worker = new BasketballClientWorker(server, client);
            return new Thread(new ThreadStart(worker.Run));
        }
    }
}