using System;
using System.Collections.Generic;
using System.Configuration;
using Grpc.Core;
using persistence;
using Ticketing;

namespace server.grpc
{
    internal static class GrpcStartServer
    {
        private const int DefaultPort = 50051;

        public static void Main(string[] args)
        {
            Console.WriteLine($"Starting gRPC server on port {DefaultPort}...");
            
            var connString = ConfigurationManager.ConnectionStrings["identifierDB"]?.ConnectionString;
            if (string.IsNullOrEmpty(connString))
            {
                Console.WriteLine("Failed to load connection string 'identifierDB'.");
                return;
            }

            Console.WriteLine($"Connection string loaded: {connString}");

            var props = new Dictionary<string, string>
            {
                { "ConnectionString", connString }
            };

            var userRepo = new UserRepositoryDb(props);
            var matchRepo = new MatchRepositoryDb(props);
            var ticketRepo = new TicketRepositoryDb(props);

            var grpcService = new GrpcBasketballServiceImpl(userRepo, matchRepo, ticketRepo);

            var server = new Grpc.Core.Server
            {
                Services = { TicketService.BindService(grpcService) },
                Ports = { new ServerPort("localhost", DefaultPort, ServerCredentials.Insecure) }
            };

            server.Start();
            Console.WriteLine($"gRPC Basketball Server running on port {DefaultPort}");
            Console.WriteLine("Press ENTER to shut down...");
            Console.ReadLine();

            server.ShutdownAsync().Wait();
        }
    }
}