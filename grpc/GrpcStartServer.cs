using System;
using System.Collections.Generic;
using Grpc.Core;
using persistence;
using services;
using Ticketing;

namespace grpc
{
    internal static class GrpcStartServer
    {
        private const int Port = 50051;

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting gRPC server on port " + Port + "...");

            var props = new Dictionary<string, string>
            {
                { "ConnectionString", "Data Source=identifier.sqlite;Version=3;" }
            };

            IMatchRepository matchRepo = new MatchRepositoryDb(props);
            ITicketRepository ticketRepo = new TicketRepositoryDb(props);

            var grpcService = new GrpcBasketballServiceImpl(matchRepo, ticketRepo);

            var server = new Grpc.Core.Server
            {
                Services = { TicketService.BindService(grpcService) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };

            server.Start();
            Console.WriteLine("gRPC Basketball Server is running on port " + Port);
            Console.WriteLine("Press ENTER to stop...");
            Console.ReadLine();

            server.ShutdownAsync().Wait();
        }
    }
}