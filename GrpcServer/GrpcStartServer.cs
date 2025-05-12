using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Grpc.Core;
using persistence;
using Ticketing;

namespace GrpcServer
{
    internal static class GrpcStartServer
    {
        private const int DefaultPort = 50051; 

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Starting gRPC server on port {DefaultPort}...");
                
                Console.WriteLine($"Current directory: {Environment.CurrentDirectory}");
                
                var connString = ConfigurationManager.ConnectionStrings["identifierDB"]?.ConnectionString;
                if (string.IsNullOrEmpty(connString))
                {
                    Console.WriteLine("Failed to load connection string 'identifierDB'.");
                    return;
                }

                Console.WriteLine($"Connection string loaded: {connString}");
                
                string dbPath = connString.Replace("URI=file:", "").Split(',')[0];
                Console.WriteLine($"Database path: {dbPath}");
                
                if (!File.Exists(dbPath))
                {
                    Console.WriteLine($"ERROR: Database file not found at: {dbPath}");
                    Console.WriteLine("Please check the path in App.config and ensure the file exists.");
                    return;
                }
                
                var props = new Dictionary<string, string>
                {
                    { "ConnectionString", connString }
                };

                try
                {
                    Console.WriteLine("Creating repositories...");
                    var userRepo = new UserRepositoryDb(props);
                    var matchRepo = new MatchRepositoryDb(props);
                    var ticketRepo = new TicketRepositoryDb(props);
                    
                    Console.WriteLine("Creating service implementation...");
                    var grpcService = new GrpcBasketballServiceImpl(userRepo, matchRepo, ticketRepo);

                    Console.WriteLine("Creating gRPC server...");
                    var server = new Server
                    {
                        Services = { Ticketing.TicketService.BindService(grpcService) },
                        Ports = { new ServerPort("localhost", DefaultPort, ServerCredentials.Insecure) }
                    };
                    
                    Console.WriteLine($"Registered service: {TicketService.Descriptor.FullName}");

                    Console.WriteLine("Starting server...");
                    server.Start();
                    Console.WriteLine($"gRPC Basketball Server running on port {DefaultPort}");
                    Console.WriteLine("Press ENTER to shut down...");
                    Console.ReadLine();
                    
                    foreach (var service in server.Services)
                    {
                        Console.WriteLine($"Service: {service}");
                    }

                    server.ShutdownAsync().Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR creating repositories or starting server: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                        Console.WriteLine($"Inner stack trace: {ex.InnerException.StackTrace}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FATAL ERROR: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}